using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligatorioIntegrador2026.Data;
using ObligatorioIntegrador2026.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ObligatorioIntegrador2026.Controllers
{
    [Authorize]
    public class MasController : Controller
    {
        private readonly AppDbContext _context;

        public MasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Movimientos()
        {
            var movimientos = await _context.Movimientos
                .Include(m => m.Colmena)
                .Include(m => m.ApiarioOrigen)
                .Include(m => m.ApiarioDestino)
                .OrderByDescending(m => m.FechaSalida)
                .ToListAsync();

            var apiarios = await _context.Apiarios.ToListAsync();

            var viewModel = new MovimientosViewModel
            {
                Vigentes = movimientos.Where(m => m.Estado == "Vigente").ToList(),
                Pasados = movimientos.Where(m => m.Estado != "Vigente").ToList(),
                Apiarios = apiarios
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetColmenasPorApiario(int apiarioId)
        {
            var colmenas = await _context.Colmenas
                .Where(c => c.ApiarioId == apiarioId)
                .Select(c => new { id = c.Id, text = string.IsNullOrEmpty(c.Identificador) ? c.CodigoEscaneo : c.Identificador })
                .ToListAsync();

            return Json(new { success = true, data = colmenas });
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarMovimiento([FromBody] MovimientoInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Datos inválidos." });
            }

            if (model.FechaRegreso.Date <= DateTime.Now.Date)
            {
                return Json(new { success = false, message = "La fecha de regreso debe ser posterior a la fecha actual." });
            }

            if (model.ApiarioOrigenId == model.ApiarioDestinoId)
            {
                return Json(new { success = false, message = "El apiario de destino no puede ser el mismo que el origen." });
            }

            var colmena = await _context.Colmenas.FindAsync(model.ColmenaId);
            if (colmena == null)
            {
                return Json(new { success = false, message = "La colmena seleccionada no existe." });
            }

            var movimiento = new Movimiento
            {
                ColmenaId = model.ColmenaId,
                ApiarioOrigenId = model.ApiarioOrigenId,
                ApiarioDestinoId = model.ApiarioDestinoId,
                Razon = model.Razon,
                FechaSalida = DateTime.Now,
                FechaRegreso = model.FechaRegreso,
                Estado = "Vigente"
            };

            // También actualizamos el ApiarioId de la colmena para reflejar que se movió físicamente
            colmena.ApiarioId = model.ApiarioDestinoId;
            _context.Colmenas.Update(colmena);

            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarEstadoMovimiento(int id, string nuevoEstado)
        {
            var movimiento = await _context.Movimientos
                .Include(m => m.Colmena)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (movimiento == null)
            {
                return Json(new { success = false, message = "Movimiento no encontrado." });
            }

            if (nuevoEstado != "Completado" && nuevoEstado != "Cancelado")
            {
                return Json(new { success = false, message = "Estado inválido." });
            }

            movimiento.Estado = nuevoEstado;

            // Si se cancela o completa, en un caso real se podría devolver la colmena al origen automáticamente.
            // Según la regla del negocio: "Para trasladarlas de un apiario a otro por un tiempo limitado, nunca permanente."
            // "Completado" significa que ya retornó al origen? O "Completado" significa que el traslado se efectuó exitosamente y se quedará ahí hasta un próximo movimiento?
            // "La fecha de regreso...". Es decir, si se completó, es porque volvió a su origen.
            // Si "Canceló", significa que nunca se movió (o se arrepintió).
            if (nuevoEstado == "Completado")
            {
                movimiento.Colmena.ApiarioId = movimiento.ApiarioDestinoId;
                _context.Colmenas.Update(movimiento.Colmena);
            }
            else if (nuevoEstado == "Cancelado")
            {
                movimiento.Colmena.ApiarioId = movimiento.ApiarioOrigenId;
                _context.Colmenas.Update(movimiento.Colmena);
            }

            _context.Movimientos.Update(movimiento);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> EditarFechaRegreso(int id, DateTime nuevaFecha)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return Json(new { success = false, message = "Movimiento no encontrado." });
            }

            if (nuevaFecha.Date <= DateTime.Now.Date)
            {
                return Json(new { success = false, message = "La nueva fecha de regreso debe ser posterior a la fecha actual." });
            }

            movimiento.FechaRegreso = nuevaFecha;
            _context.Movimientos.Update(movimiento);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        public async Task<IActionResult> CompararApiarios()
        {
            var apiarios = await _context.Apiarios
                .Include(a => a.Colmenas)
                .ToListAsync();
            return View(apiarios);
        }

        public async Task<IActionResult> CompararColmenas()
        {
            var colmenas = await _context.Colmenas
                .Include(c => c.Apiario)
                .Include(c => c.NotasTecnicas)
                .ToListAsync();

            var latestTreatments = await _context.Treatments
                .GroupBy(t => t.ColmenaId)
                .Select(g => new { ColmenaId = g.Key, LatestDate = g.Max(t => t.Fecha) })
                .ToDictionaryAsync(x => x.ColmenaId, x => x.LatestDate);

            var colmenasDto = colmenas.Select(c => new {
                id = c.Id,
                identificador = c.Identificador,
                codigoEscaneo = c.CodigoEscaneo,
                apiarioOrigen = c.Apiario?.Nombre ?? "Sin Apiario",
                estado = c.Estado,
                pesoKg = c.PesoKg,
                temperaturaInterna = c.TemperaturaInterna,
                humedadInterna = c.HumedadInterna,
                produccionMielKg = c.ProduccionMielKg,
                esPiloto = c.EsPiloto,
                cantidadAbejas = c.CantidadAbejas,
                comportamientoAbejas = c.ComportamientoAbejas,
                estadoReina = c.EstadoReina,
                fechaUltimoTratamiento = latestTreatments.TryGetValue(c.Id, out var tDate) ? tDate.ToString("dd/MM/yyyy") : "N/A",
                fechaUltimaNotaTecnica = c.NotasTecnicas.Any() ? c.NotasTecnicas.Max(n => n.Fecha).ToString("dd/MM/yyyy") : "N/A"
            }).ToList();

            ViewBag.ColmenasJson = System.Text.Json.JsonSerializer.Serialize(colmenasDto);
            return View();
        }

        public async Task<IActionResult> Finanzacion()
        {
            var active = await _context.Analisis
                .Include(a => a.Inversiones)
                .Include(a => a.Ganancias)
                .FirstOrDefaultAsync(a => a.FechaFin == null);

            var history = await _context.Analisis
                .Include(a => a.Inversiones)
                .Include(a => a.Ganancias)
                .Where(a => a.FechaFin != null)
                .OrderByDescending(a => a.FechaFin)
                .ToListAsync();

            var viewModel = new FinanzacionViewModel
            {
                AnalisisActivo = active,
                HistorialAnalisis = history
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> IniciarAnalisis()
        {
            var activeExists = await _context.Analisis.AnyAsync(a => a.FechaFin == null);
            if (activeExists)
            {
                return Json(new { success = false, message = "Ya existe un análisis activo." });
            }

            var nuevo = new Analisis
            {
                FechaInicio = DateTime.Now,
                FechaFin = null
            };

            _context.Analisis.Add(nuevo);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> FinalizarAnalisis(int id, bool iniciarNuevo)
        {
            var analisis = await _context.Analisis.FindAsync(id);
            if (analisis == null)
            {
                return Json(new { success = false, message = "Análisis no encontrado." });
            }

            if (analisis.FechaFin != null)
            {
                return Json(new { success = false, message = "El análisis ya está finalizado." });
            }

            analisis.FechaFin = DateTime.Now;
            _context.Analisis.Update(analisis);

            if (iniciarNuevo)
            {
                var nuevo = new Analisis
                {
                    FechaInicio = DateTime.Now,
                    FechaFin = null
                };
                _context.Analisis.Add(nuevo);
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarInversion([FromBody] InversionInputModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Titulo) || model.Precio <= 0)
            {
                return Json(new { success = false, message = "Datos de inversión inválidos." });
            }

            var active = await _context.Analisis.FirstOrDefaultAsync(a => a.Id == model.AnalisisId && a.FechaFin == null);
            if (active == null)
            {
                return Json(new { success = false, message = "No hay un análisis activo para registrar inversiones." });
            }

            var inversion = new Inversion
            {
                AnalisisId = model.AnalisisId,
                Titulo = model.Titulo,
                Nota = model.Nota ?? string.Empty,
                Precio = model.Precio
            };

            _context.Inversiones.Add(inversion);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarGanancia([FromBody] GananciaInputModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Titulo) || model.Monto <= 0)
            {
                return Json(new { success = false, message = "Datos de ganancia inválidos." });
            }

            var active = await _context.Analisis.FirstOrDefaultAsync(a => a.Id == model.AnalisisId && a.FechaFin == null);
            if (active == null)
            {
                return Json(new { success = false, message = "No hay un análisis activo para registrar ganancias." });
            }

            var ganancia = new Ganancia
            {
                AnalisisId = model.AnalisisId,
                Titulo = model.Titulo,
                Descripcion = model.Descripcion ?? string.Empty,
                Monto = model.Monto
            };

            _context.Ganancias.Add(ganancia);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> EditarInversion([FromBody] InversionInputModel model)
        {
            if (model == null || model.Id == null || string.IsNullOrWhiteSpace(model.Titulo) || model.Precio <= 0)
            {
                return Json(new { success = false, message = "Datos de inversión inválidos." });
            }

            var inversion = await _context.Inversiones.FirstOrDefaultAsync(i => i.Id == model.Id);
            if (inversion == null)
            {
                return Json(new { success = false, message = "Inversión no encontrada." });
            }

            inversion.Titulo = model.Titulo;
            inversion.Nota = model.Nota ?? string.Empty;
            inversion.Precio = model.Precio;

            _context.Inversiones.Update(inversion);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> EditarGanancia([FromBody] GananciaInputModel model)
        {
            if (model == null || model.Id == null || string.IsNullOrWhiteSpace(model.Titulo) || model.Monto <= 0)
            {
                return Json(new { success = false, message = "Datos de ganancia inválidos." });
            }

            var ganancia = await _context.Ganancias.FirstOrDefaultAsync(g => g.Id == model.Id);
            if (ganancia == null)
            {
                return Json(new { success = false, message = "Ganancia no encontrada." });
            }

            ganancia.Titulo = model.Titulo;
            ganancia.Descripcion = model.Descripcion ?? string.Empty;
            ganancia.Monto = model.Monto;

            _context.Ganancias.Update(ganancia);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetAnalisisDetalles(int id)
        {
            var analisis = await _context.Analisis
                .Include(a => a.Inversiones)
                .Include(a => a.Ganancias)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (analisis == null)
            {
                return Json(new { success = false, message = "Análisis no encontrado." });
            }

            var data = new
            {
                id = analisis.Id,
                fechaInicio = analisis.FechaInicio.ToString("dd/MM/yyyy HH:mm"),
                fechaFin = analisis.FechaFin?.ToString("dd/MM/yyyy HH:mm") ?? "En curso",
                totalInversion = analisis.TotalInversion,
                gananciaBruta = analisis.GananciaBruta,
                balanceNeto = analisis.BalanceNeto,
                inversiones = analisis.Inversiones.Select(i => new {
                    titulo = i.Titulo,
                    nota = i.Nota,
                    precio = i.Precio
                }).ToList(),
                ganancias = analisis.Ganancias.Select(g => new {
                    titulo = g.Titulo,
                    descripcion = g.Descripcion,
                    monto = g.Monto
                }).ToList()
            };

            return Json(new { success = true, data = data });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarAnalisis(int id)
        {
            var analisis = await _context.Analisis
                .Include(a => a.Inversiones)
                .Include(a => a.Ganancias)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (analisis == null)
            {
                return Json(new { success = false, message = "Análisis no encontrado." });
            }

            if (analisis.Inversiones != null)
                _context.Inversiones.RemoveRange(analisis.Inversiones);
            if (analisis.Ganancias != null)
                _context.Ganancias.RemoveRange(analisis.Ganancias);

            _context.Analisis.Remove(analisis);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarInversion(int id)
        {
            var inv = await _context.Inversiones.FindAsync(id);
            if (inv == null)
            {
                return Json(new { success = false, message = "Inversión no encontrada." });
            }

            // Ensure it belongs to an active analysis
            var active = await _context.Analisis.AnyAsync(a => a.Id == inv.AnalisisId && a.FechaFin == null);
            if (!active)
            {
                return Json(new { success = false, message = "Solo se pueden eliminar inversiones de un análisis activo." });
            }

            _context.Inversiones.Remove(inv);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarGanancia(int id)
        {
            var gan = await _context.Ganancias.FindAsync(id);
            if (gan == null)
            {
                return Json(new { success = false, message = "Ganancia no encontrada." });
            }

            // Ensure it belongs to an active analysis
            var active = await _context.Analisis.AnyAsync(a => a.Id == gan.AnalisisId && a.FechaFin == null);
            if (!active)
            {
                return Json(new { success = false, message = "Solo se pueden eliminar ganancias de un análisis activo." });
            }

            _context.Ganancias.Remove(gan);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        public IActionResult Inventario()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipments(string? search, string? category, string? type, string? status, string? stockFilter, string? sortBy)
        {
            var query = _context.Equipments.AsQueryable();

            if (!string.IsNullOrEmpty(category) && category != "all")
            {
                query = query.Where(e => e.Category == category);
            }

            if (!string.IsNullOrEmpty(type) && type != "all")
            {
                query = query.Where(e => e.Type == type);
            }

            if (!string.IsNullOrEmpty(stockFilter) && stockFilter != "all")
            {
                if (stockFilter == "out")
                {
                    query = query.Where(e => e.Stock == 0);
                }
                else if (stockFilter == "available")
                {
                    query = query.Where(e => e.Stock > 0);
                }
            }

            // Sorting
            if (sortBy == "stockAsc")
            {
                query = query.OrderBy(e => e.Stock);
            }
            else if (sortBy == "stockDesc")
            {
                query = query.OrderByDescending(e => e.Stock);
            }
            else if (sortBy == "nameAsc")
            {
                query = query.OrderBy(e => e.Name);
            }
            else if (sortBy == "nameDesc")
            {
                query = query.OrderByDescending(e => e.Name);
            }
            else if (sortBy == "priceAsc")
            {
                query = query.OrderBy(e => e.UnitPrice);
            }
            else if (sortBy == "priceDesc")
            {
                query = query.OrderByDescending(e => e.UnitPrice);
            }
            else if (sortBy == "default")
            {
                query = query.OrderBy(e => e.DisplayOrder);
            }
            else
            {
                query = query.OrderBy(e => e.Id);
            }

            var list = await query.ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                var cleanSearch = RemoveAccents(search).ToLower();
                list = list.Where(e => RemoveAccents(e.Name).ToLower().Contains(cleanSearch)).ToList();
            }

            if (!string.IsNullOrEmpty(status) && status != "all")
            {
                list = list.Where(e => e.Status == status).ToList();
            }

            // Get unique types for filter dropdown
            var allTypes = await _context.Equipments
                .Select(e => e.Type)
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync();

            return Json(new { success = true, data = list, types = allTypes });
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipment(int id)
        {
            var item = await _context.Equipments.FindAsync(id);
            if (item == null)
            {
                return Json(new { success = false, message = "Equipamiento no encontrado." });
            }
            return Json(new { success = true, data = item });
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipment([FromBody] Equipment model)
        {
            if (model == null)
            {
                return Json(new { success = false, errors = new[] { "Datos inválidos." } });
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("Name", "La descripción/nombre es obligatoria.");
            }
            if (string.IsNullOrWhiteSpace(model.Type))
            {
                ModelState.AddModelError("Type", "El tipo es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(model.Category))
            {
                ModelState.AddModelError("Category", "La categoría es obligatoria.");
            }

            if (model.Stock < 0)
            {
                ModelState.AddModelError("Stock", "El stock no puede ser negativo.");
            }

            if (model.LowThreshold < 0)
            {
                ModelState.AddModelError("LowThreshold", "El umbral bajo no puede ser negativo.");
            }

            if (model.MediumThreshold < 0)
            {
                ModelState.AddModelError("MediumThreshold", "El umbral medio no puede ser negativo.");
            }

            if (model.MediumThreshold <= model.LowThreshold)
            {
                ModelState.AddModelError("MediumThreshold", "El umbral medio debe ser mayor que el umbral bajo.");
            }

            if (model.UnitPrice < 0)
            {
                ModelState.AddModelError("UnitPrice", "El precio unitario no puede ser negativo.");
            }

            if (string.IsNullOrWhiteSpace(model.Currency) || (model.Currency != "UYU" && model.Currency != "USD"))
            {
                ModelState.AddModelError("Currency", "La moneda seleccionada no es válida.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }

            // Calculate display order
            int maxOrder = await _context.Equipments.AnyAsync() ? await _context.Equipments.MaxAsync(e => e.DisplayOrder) : 0;
            model.DisplayOrder = maxOrder + 1;

            _context.Equipments.Add(model);
            await _context.SaveChangesAsync();

            // Registrar inversión automática si hay un análisis activo y el stock inicial es mayor a 0
            if (model.Stock > 0)
            {
                var activeAnalysis = await _context.Analisis.FirstOrDefaultAsync(a => a.FechaFin == null);
                if (activeAnalysis != null)
                {
                    double unitPriceUYU = model.Currency == "USD" ? model.UnitPrice * 40.0 : model.UnitPrice;
                    var inversion = new Inversion
                    {
                        AnalisisId = activeAnalysis.Id,
                        Titulo = $"Compra inicial: {model.Name}",
                        Nota = $"Adquisición de {model.Stock} unidades en inventario" + (model.Currency == "USD" ? $" (US$ {model.UnitPrice} c/u, cotiz. $40)" : ""),
                        Precio = model.Stock * unitPriceUYU
                    };
                    _context.Inversiones.Add(inversion);
                    await _context.SaveChangesAsync();
                }
            }

            return Json(new { success = true, data = model });
        }

        [HttpPost]
        public async Task<IActionResult> EditEquipment([FromBody] Equipment model)
        {
            if (model == null)
            {
                return Json(new { success = false, errors = new[] { "Datos inválidos." } });
            }

            var item = await _context.Equipments.FindAsync(model.Id);
            if (item == null)
            {
                return Json(new { success = false, errors = new[] { "Equipamiento no encontrado." } });
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("Name", "La descripción/nombre es obligatoria.");
            }
            if (string.IsNullOrWhiteSpace(model.Type))
            {
                ModelState.AddModelError("Type", "El tipo es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(model.Category))
            {
                ModelState.AddModelError("Category", "La categoría es obligatoria.");
            }

            if (model.Stock < 0)
            {
                ModelState.AddModelError("Stock", "El stock no puede ser negativo.");
            }

            if (model.LowThreshold < 0)
            {
                ModelState.AddModelError("LowThreshold", "El umbral bajo no puede ser negativo.");
            }

            if (model.MediumThreshold < 0)
            {
                ModelState.AddModelError("MediumThreshold", "El umbral medio no puede ser negativo.");
            }

            if (model.MediumThreshold <= model.LowThreshold)
            {
                ModelState.AddModelError("MediumThreshold", "El umbral medio debe ser mayor que el umbral bajo.");
            }

            if (model.UnitPrice < 0)
            {
                ModelState.AddModelError("UnitPrice", "El precio unitario no puede ser negativo.");
            }

            if (string.IsNullOrWhiteSpace(model.Currency) || (model.Currency != "UYU" && model.Currency != "USD"))
            {
                ModelState.AddModelError("Currency", "La moneda seleccionada no es válida.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }

            int oldStock = item.Stock;

            item.Name = model.Name;
            item.Type = model.Type;
            item.Stock = model.Stock;
            item.Category = model.Category;
            item.LowThreshold = model.LowThreshold;
            item.MediumThreshold = model.MediumThreshold;
            item.UnitPrice = model.UnitPrice;
            item.Currency = model.Currency;

            _context.Equipments.Update(item);
            await _context.SaveChangesAsync();

            // Registrar inversión automática si hay un análisis activo y el stock aumentó
            if (model.Stock > oldStock)
            {
                var activeAnalysis = await _context.Analisis.FirstOrDefaultAsync(a => a.FechaFin == null);
                if (activeAnalysis != null)
                {
                    int diff = model.Stock - oldStock;
                    double unitPriceUYU = model.Currency == "USD" ? model.UnitPrice * 40.0 : model.UnitPrice;
                    var inversion = new Inversion
                    {
                        AnalisisId = activeAnalysis.Id,
                        Titulo = $"Adquisición de stock: {item.Name}",
                        Nota = $"Reposición de {diff} unidades en inventario" + (model.Currency == "USD" ? $" (US$ {model.UnitPrice} c/u, cotiz. $40)" : ""),
                        Precio = diff * unitPriceUYU
                    };
                    _context.Inversiones.Add(inversion);
                    await _context.SaveChangesAsync();
                }
            }

            return Json(new { success = true, data = item });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var item = await _context.Equipments.FindAsync(id);
            if (item == null)
            {
                return Json(new { success = false, message = "Equipamiento no encontrado." });
            }

            _context.Equipments.Remove(item);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> CheckConflict(int id, string name, string type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(new { success = true, hasConflict = false });
            }

            name = name.Trim().ToLower();
            type = type.Trim().ToLower();

            var query = _context.Equipments.AsQueryable();
            if (id > 0)
            {
                query = query.Where(e => e.Id != id);
            }

            // Check if there is an item with the same name
            var match = await query.FirstOrDefaultAsync(e => e.Name.ToLower() == name);
            if (match != null)
            {
                // We check if the Name and Type matches as well
                bool exactBoth = match.Type.ToLower() == type;
                return Json(new { 
                    success = true, 
                    hasConflict = true, 
                    exactBoth = exactBoth,
                    conflictName = match.Name,
                    conflictType = match.Type
                });
            }

            return Json(new { success = true, hasConflict = false });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder([FromBody] List<int> sortedIds)
        {
            if (sortedIds == null || sortedIds.Count == 0)
            {
                return Json(new { success = false, message = "Datos vacíos." });
            }

            var equipments = await _context.Equipments.ToListAsync();
            for (int i = 0; i < sortedIds.Count; i++)
            {
                var id = sortedIds[i];
                var item = equipments.FirstOrDefault(e => e.Id == id);
                if (item != null)
                {
                    item.DisplayOrder = i + 1;
                    _context.Equipments.Update(item);
                }
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        private static string RemoveAccents(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            var normalizedString = text.Normalize(System.Text.NormalizationForm.FormD);
            var stringBuilder = new System.Text.StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }
    }
}
