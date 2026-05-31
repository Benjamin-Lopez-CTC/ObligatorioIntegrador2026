using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligatorioIntegrador2026.Data;
using ObligatorioIntegrador2026.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ObligatorioIntegrador2026.Controllers
{
    [Authorize]
    public class ColmenasController : Controller
    {
        private readonly AppDbContext _context;

        public ColmenasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var colmenas = await _context.Colmenas.Include(c => c.Apiario).ToListAsync();
            var apiarios = await _context.Apiarios.ToListAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var colmenasJson = JsonSerializer.Serialize(colmenas.Select(c => new {
                id = c.Id,
                identificador = c.Identificador,
                codigoEscaneo = c.CodigoEscaneo,
                apiarioNombre = c.Apiario?.Nombre,
                apiarioId = c.ApiarioId,
                estado = c.Estado,
                peso = c.PesoKg.ToString("0.0"),
                temp = c.TemperaturaInterna.ToString("0.0"),
                comportamiento = c.ComportamientoAbejas,
                esPiloto = c.EsPiloto
            }), options);

            ViewBag.ColmenasJson = colmenasJson;
            ViewBag.Apiarios = apiarios;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarNueva([Bind("Identificador,ApiarioId,CantidadAbejas,UbicacionIntraApiario,EstadoReina,ComportamientoAbejas,EsPiloto")] Colmena colmena, string? returnUrl = null)
        {
            ModelState.Remove("Apiario");
            ModelState.Remove("CodigoEscaneo");
            ModelState.Remove("Estado");

            if (ModelState.IsValid)
            {
                colmena.Estado = "Óptimo"; // Default
                colmena.PesoKg = 0;
                // Leave EsPiloto as what the form bound it to (true/false)
                colmena.TemperaturaInterna = colmena.EsPiloto ? 35.0 : 0; // Default nominal temp if pilot
                colmena.HumedadInterna = colmena.EsPiloto ? 50.0 : 0;
                colmena.ProduccionMielKg = 0;
                
                // Generate a unique 6 digit code
                colmena.CodigoEscaneo = new Random().Next(100000, 999999).ToString();

                _context.Add(colmena);
                await _context.SaveChangesAsync();
                
                TempData["SuccessMessage"] = "Colmena registrada exitosamente.";
                if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
                return RedirectToAction(nameof(Index));
            }
            
            TempData["ErrorMessage"] = "Error al registrar la colmena. Revise los datos e intente nuevamente.";
            if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var colmena = await _context.Colmenas.FindAsync(id);
            if (colmena != null)
            {
                _context.Colmenas.Remove(colmena);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Colmena eliminada exitosamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "La colmena no fue encontrada.";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detalles(int id)
        {
            var colmena = await _context.Colmenas.Include(c => c.Apiario).Include(c => c.NotasTecnicas).FirstOrDefaultAsync(c => c.Id == id);
            if (colmena == null) return NotFound();

            ActualizarEstadoAutomatico(colmena);
            await _context.SaveChangesAsync();

            ViewBag.Tratamientos = await _context.Treatments
                .Where(t => t.ColmenaId == id)
                .OrderByDescending(t => t.Fecha)
                .Take(2)
                .ToListAsync();

            var equipments = await _context.Equipments.OrderBy(e => e.DisplayOrder).ToListAsync();
            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };
            ViewBag.EquipmentsJson = System.Text.Json.JsonSerializer.Serialize(equipments, options);

            return View(colmena);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarColmena(int id, double? temperatura, double? humedad, double peso, double produccion, string comportamiento)
        {
            var colmena = await _context.Colmenas.Include(c => c.NotasTecnicas).FirstOrDefaultAsync(c => c.Id == id);
            if (colmena == null) return NotFound();

            if (colmena.EsPiloto)
            {
                if (temperatura.HasValue) colmena.TemperaturaInterna = temperatura.Value;
                if (humedad.HasValue) colmena.HumedadInterna = humedad.Value;
            }

            colmena.PesoKg = peso;
            colmena.ProduccionMielKg = produccion;
            colmena.ComportamientoAbejas = comportamiento;

            ActualizarEstadoAutomatico(colmena);

            var apiario = await _context.Apiarios.FindAsync(colmena.ApiarioId);
            if (apiario != null)
            {
                apiario.UltimaInspeccion = DateTime.Now;
                _context.Apiarios.Update(apiario);
            }

            await _context.SaveChangesAsync();
            
            TempData["SuccessMessage"] = "Colmena actualizada exitosamente.";
            return RedirectToAction(nameof(Detalles), new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AñadirNotaTecnica(int id, string detalles, string estadoReina, string estadoColmena)
        {
            var colmena = await _context.Colmenas.Include(c => c.NotasTecnicas).FirstOrDefaultAsync(c => c.Id == id);
            if (colmena == null) return NotFound();

            var nuevaNota = new NotaTecnica
            {
                ColmenaId = id,
                Detalles = detalles,
                EstadoReina = estadoReina,
                EstadoColmena = estadoColmena,
                Fecha = DateTime.Now
            };

            _context.NotasTecnicas.Add(nuevaNota);
            
            colmena.EstadoReina = estadoReina;
            
            // Allow manual override of state unless automatic rules force it to Alert
            colmena.Estado = estadoColmena;

            colmena.NotasTecnicas.Add(nuevaNota);

            ActualizarEstadoAutomatico(colmena);

            var apiario = await _context.Apiarios.FindAsync(colmena.ApiarioId);
            if (apiario != null)
            {
                apiario.UltimaInspeccion = DateTime.Now;
                _context.Apiarios.Update(apiario);
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Nota técnica agregada correctamente.";
            return RedirectToAction(nameof(Detalles), new { id = id });
        }

        public async Task<IActionResult> Tratamientos(int id)
        {
            var colmena = await _context.Colmenas.Include(c => c.Apiario).FirstOrDefaultAsync(c => c.Id == id);
            if (colmena == null) return NotFound();

            var equipments = await _context.Equipments.OrderBy(e => e.DisplayOrder).ToListAsync();
            
            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };
            ViewBag.EquipmentsJson = System.Text.Json.JsonSerializer.Serialize(equipments, options);

            return View(colmena);
        }

        [HttpGet]
        public async Task<IActionResult> GetTratamientosList(int id)
        {
            var list = await _context.Treatments
                .Where(t => t.ColmenaId == id)
                .Select(t => new {
                    id = t.Id,
                    titulo = t.Titulo,
                    tipo = t.Tipo,
                    nota = t.Nota,
                    fecha = t.Fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                    fechaDisplay = t.Fecha.ToString("dd MMM yyyy HH:mm").ToUpper(),
                    equipamientos = t.EquipamientosUsados.Select(eq => new {
                        id = eq.Id,
                        name = eq.EquipmentName,
                        cantidad = eq.Cantidad
                    }).ToList()
                })
                .ToListAsync();

            return Json(new { success = true, data = list });
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarTratamiento([FromBody] TreatmentInputModel model)
        {
            if (model == null)
            {
                return Json(new { success = false, errors = new[] { "Datos inválidos." } });
            }

            if (string.IsNullOrWhiteSpace(model.Titulo))
            {
                return Json(new { success = false, errors = new[] { "El título del tratamiento es obligatorio." } });
            }

            if (string.IsNullOrWhiteSpace(model.Tipo))
            {
                return Json(new { success = false, errors = new[] { "El tipo de tratamiento es obligatorio." } });
            }

            // Filter out empty rows (where equipment id is not selected or zero)
            var activeEquipments = model.Equipamientos.Where(e => e.EquipmentId > 0).ToList();

            // Validate that if quantity is specified, equipment is also selected, and vice-versa
            foreach (var eq in activeEquipments)
            {
                if (eq.Cantidad <= 0)
                {
                    return Json(new { success = false, errors = new[] { "La cantidad del equipamiento debe ser mayor que cero." } });
                }
            }

            // Check for duplicate equipments
            var duplicates = activeEquipments.GroupBy(e => e.EquipmentId).Where(g => g.Count() > 1).ToList();
            if (duplicates.Any())
            {
                return Json(new { success = false, errors = new[] { "No se puede seleccionar el mismo equipamiento más de una vez." } });
            }

            // Verify stock and existence
            foreach (var eq in activeEquipments)
            {
                var dbEq = await _context.Equipments.FindAsync(eq.EquipmentId);
                if (dbEq == null)
                {
                    return Json(new { success = false, errors = new[] { "Uno de los equipamientos seleccionados no existe en el inventario." } });
                }
                if (eq.Cantidad > dbEq.Stock)
                {
                    return Json(new { success = false, errors = new[] { $"La cantidad solicitada de '{dbEq.Name}' ({eq.Cantidad}u) supera el stock disponible ({dbEq.Stock}u)." } });
                }
            }

            // Create treatment
            var treatment = new Treatment
            {
                ColmenaId = model.ColmenaId,
                Titulo = model.Titulo,
                Tipo = model.Tipo,
                Nota = model.Nota,
                Fecha = DateTime.Now
            };

            // Deduct stock and add treatment equipment copies
            foreach (var eq in activeEquipments)
            {
                var dbEq = await _context.Equipments.FindAsync(eq.EquipmentId);
                if (dbEq != null)
                {
                    dbEq.Stock -= eq.Cantidad;
                    _context.Equipments.Update(dbEq);

                    treatment.EquipamientosUsados.Add(new TreatmentEquipment
                    {
                        EquipmentName = dbEq.Name,
                        Cantidad = eq.Cantidad
                    });
                }
            }

            _context.Treatments.Add(treatment);

            var colmena = await _context.Colmenas.FindAsync(model.ColmenaId);
            if (colmena != null)
            {
                var apiario = await _context.Apiarios.FindAsync(colmena.ApiarioId);
                if (apiario != null)
                {
                    apiario.UltimaInspeccion = DateTime.Now;
                    _context.Apiarios.Update(apiario);
                }
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Tratamiento registrado exitosamente." });
        }

        private void ActualizarEstadoAutomatico(Colmena colmena)
        {
            bool alertReason = false;

            if (colmena.EsPiloto)
            {
                if (colmena.TemperaturaInterna < 30 || colmena.TemperaturaInterna > 38) alertReason = true;
                if (colmena.HumedadInterna < 40 || colmena.HumedadInterna > 80) alertReason = true;
            }

            if (colmena.EstadoReina == "Ausente") alertReason = true;

            if (colmena.NotasTecnicas != null && colmena.NotasTecnicas.Any())
            {
                var ultimaNota = colmena.NotasTecnicas.OrderByDescending(n => n.Fecha).First();
                if ((DateTime.Now - ultimaNota.Fecha).TotalDays > 30) alertReason = true;
            }
            else
            {
                alertReason = true; // No notes ever means alert
            }

            // Only downgrade to Alerta, never upgrade from Crítico to Alerta just because of this
            if (alertReason && colmena.Estado == "Óptimo")
            {
                colmena.Estado = "Alerta";
            }
        }
    }

    public class TreatmentInputModel
    {
        public int ColmenaId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string? Nota { get; set; }
        public List<EquipmentInputModel> Equipamientos { get; set; } = new List<EquipmentInputModel>();
    }

    public class EquipmentInputModel
    {
        public int EquipmentId { get; set; }
        public int Cantidad { get; set; }
    }
}
