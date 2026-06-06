using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligatorioIntegrador2026.Data;
using ObligatorioIntegrador2026.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;

namespace ObligatorioIntegrador2026.Controllers
{
    [Authorize]
    public class ColmenasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ColmenasController(AppDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
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
                esPiloto = c.EsPiloto,
                esNucleo = c.EsNucleo
            }), options);

            ViewBag.ColmenasJson = colmenasJson;
            ViewBag.Apiarios = apiarios;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarNueva([Bind("Identificador,ApiarioId,CantidadAbejas,UbicacionIntraApiario,EstadoReina,ComportamientoAbejas,EsPiloto,EsNucleo,Alzas,MediasAlzas,AlzasTresCuartos,TemperaturaInterna,HumedadInterna,ProduccionMielKg")] Colmena colmena, string? returnUrl = null)
        {
            ModelState.Remove("Apiario");
            ModelState.Remove("CodigoEscaneo");
            ModelState.Remove("Estado");

            if (!colmena.EsPiloto)
            {
                ModelState.Remove("TemperaturaInterna");
                ModelState.Remove("HumedadInterna");
            }

            if (ModelState.IsValid)
            {
                colmena.Estado = "Óptimo"; // Default
                colmena.PesoKg = 40.0; // Valor normal estándar en lugar de 0
                
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

        public async Task<IActionResult> Detalles(int id, int? fromApiarioId = null)
        {
            var colmena = await _context.Colmenas.Include(c => c.Apiario).Include(c => c.NotasTecnicas).FirstOrDefaultAsync(c => c.Id == id);
            if (colmena == null) return NotFound();

            ActualizarEstadoAutomatico(colmena);
            await _context.SaveChangesAsync();

            ViewBag.FromApiarioId = fromApiarioId;

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
        public async Task<IActionResult> EditarColmena(int id, double? temperatura, double? humedad, double peso, double produccion, string comportamiento, bool esNucleo = false)
        {
            var colmena = await _context.Colmenas.Include(c => c.NotasTecnicas).FirstOrDefaultAsync(c => c.Id == id);
            if (colmena == null) return NotFound();

            if (colmena.EsPiloto)
            {
                if (temperatura.HasValue) colmena.TemperaturaInterna = temperatura.Value;
                if (humedad.HasValue) colmena.HumedadInterna = humedad.Value;
            }

            colmena.PesoKg = peso;
            
            double maxMiel = (colmena.Alzas * 22.0) + (colmena.MediasAlzas * 12.0) + (colmena.AlzasTresCuartos * 17.0);
            colmena.ProduccionMielKg = Math.Min(produccion, maxMiel);
            
            colmena.ComportamientoAbejas = comportamiento;
            colmena.EsNucleo = esNucleo;

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
        public async Task<IActionResult> AñadirNotaTecnica(int id, string detalles, string estadoReina, string estadoColmena, double? temperatura = null, double? humedad = null, int alzasCosechadas = 0, int mediasAlzasCosechadas = 0, int alzasTresCuartosCosechadas = 0)
        {
            var colmena = await _context.Colmenas.Include(c => c.NotasTecnicas).FirstOrDefaultAsync(c => c.Id == id);
            if (colmena == null) return NotFound();
            double kilosCosechados = (alzasCosechadas * 22.0) + (alzasTresCuartosCosechadas * 17.0) + (mediasAlzasCosechadas * 12.0);
            kilosCosechados = Math.Min(kilosCosechados, colmena.ProduccionMielKg);

            var nuevaNota = new NotaTecnica
            {
                ColmenaId = id,
                Detalles = detalles,
                EstadoReina = estadoReina,
                EstadoColmena = estadoColmena,
                Fecha = DateTime.Now,
                Temperatura = temperatura,
                Humedad = humedad,
                AlzasCosechadas = alzasCosechadas,
                MediasAlzasCosechadas = mediasAlzasCosechadas,
                AlzasTresCuartosCosechadas = alzasTresCuartosCosechadas,
                KilosCosechados = kilosCosechados
            };

            _context.NotasTecnicas.Add(nuevaNota);
            
            // No se restan las alzas físicas, solo se descuenta la miel producida
            colmena.ProduccionMielKg = Math.Max(0, colmena.ProduccionMielKg - kilosCosechados);

            colmena.EstadoReina = estadoReina;
            
            // Allow manual override of state unless automatic rules force it to Alert
            colmena.Estado = estadoColmena;

            if (temperatura.HasValue) colmena.TemperaturaInterna = temperatura.Value;
            if (humedad.HasValue) colmena.HumedadInterna = humedad.Value;

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

        public async Task<IActionResult> Tratamientos(int id, int? fromApiarioId = null)
        {
            var colmena = await _context.Colmenas.Include(c => c.Apiario).FirstOrDefaultAsync(c => c.Id == id);
            if (colmena == null) return NotFound();

            ViewBag.FromApiarioId = fromApiarioId;

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

        [HttpPost]
        public async Task<IActionResult> RecognizeCode([FromBody] RecognizeCodeRequest request)
        {
            if (string.IsNullOrEmpty(request?.Base64Image))
            {
                return BadRequest("No image provided");
            }

            try
            {
                var apiKey = _configuration["Gemini:ApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    return StatusCode(500, "Gemini API key is not configured.");
                }

                var cleanBase64 = request.Base64Image;
                if (cleanBase64.Contains(","))
                {
                    cleanBase64 = cleanBase64.Substring(cleanBase64.IndexOf(",") + 1);
                }

                var payload = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new object[]
                            {
                                new { text = "Extract only the 6 numerical digits written in this image. Do not include any other text or characters. If there are no digits, return empty." },
                                new
                                {
                                    inline_data = new
                                    {
                                        mime_type = "image/jpeg",
                                        data = cleanBase64
                                    }
                                }
                            }
                        }
                    }
                };

                var client = _httpClientFactory.CreateClient();
                var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";
                
                var response = await client.PostAsJsonAsync(url, payload);
                if (!response.IsSuccessStatusCode)
                {
                    var errorStr = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"Error from Gemini: {errorStr}");
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(jsonResponse);
                
                string extractedText = "";
                if (doc.RootElement.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                {
                    var firstCandidate = candidates[0];
                    if (firstCandidate.TryGetProperty("content", out var content) && 
                        content.TryGetProperty("parts", out var parts) && parts.GetArrayLength() > 0)
                    {
                        var firstPart = parts[0];
                        if (firstPart.TryGetProperty("text", out var textProp))
                        {
                            extractedText = textProp.GetString() ?? "";
                        }
                    }
                }

                var digits = new string(extractedText.Where(char.IsDigit).ToArray());
                
                return Ok(new { success = true, digits = digits });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class RecognizeCodeRequest
    {
        public string Base64Image { get; set; }
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
