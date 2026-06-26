using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligatorioIntegrador2026.Data;
using ObligatorioIntegrador2026.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ObligatorioIntegrador2026.Controllers
{
    [Authorize]
    public class ExtraccionesController : Controller
    {
        private readonly AppDbContext _context;

        public ExtraccionesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var extracciones = await _context.Extracciones
                .Include(e => e.Ganancia)
                .OrderByDescending(e => e.Fecha)
                .ToListAsync();

            var apiarios = await _context.Apiarios.Select(a => new { a.Id, a.Nombre }).ToListAsync();
            
            var colmenasInfo = await _context.Colmenas
                .Where(c => c.Alzas > 0 || c.MediasAlzas > 0 || c.AlzasTresCuartos > 0)
                .Select(c => new {
                    c.Id,
                    c.Identificador,
                    c.CodigoEscaneo,
                    c.ApiarioId,
                    c.Alzas,
                    c.MediasAlzas,
                    c.AlzasTresCuartos
                })
                .ToListAsync();

            var analisisActivo = await _context.Analisis.FirstOrDefaultAsync(a => a.FechaFin == null);
            var ultimoCerrado = await _context.Analisis.Where(a => a.FechaFin != null).OrderByDescending(a => a.FechaFin).FirstOrDefaultAsync();

            ViewBag.ApiariosJson = JsonSerializer.Serialize(apiarios);
            ViewBag.ColmenasJson = JsonSerializer.Serialize(colmenasInfo);
            ViewBag.AnalisisActivoId = analisisActivo?.Id;
            ViewBag.UltimoCerradoId = ultimoCerrado?.Id;
            ViewBag.UltimoCerradoNombre = ultimoCerrado != null ? $"Cierre: {ultimoCerrado.FechaFin?.ToString("dd/MM/yyyy")}" : null;

            return View(extracciones);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarCosechaMasa([FromBody] CosechaMasaDto dto)
        {
            if (dto == null || dto.Colmenas == null || !dto.Colmenas.Any())
            {
                return Json(new { success = false, message = "No se enviaron datos válidos." });
            }

            int? analisisDestinoId = dto.AnalisisId;
            
            if (!analisisDestinoId.HasValue && dto.CreateNewAnalisis)
            {
                var newAnalisis = new Analisis
                {
                    FechaInicio = DateTime.Now
                };
                _context.Analisis.Add(newAnalisis);
                await _context.SaveChangesAsync();
                analisisDestinoId = newAnalisis.Id;
            }

            var extraccion = new Extraccion
            {
                Fecha = DateTime.Now,
                CantidadColmenasCosechadas = dto.Colmenas.Count,
                Notas = dto.Notas
            };

            double totalKilos = 0;

            foreach (var cDto in dto.Colmenas)
            {
                var colmena = await _context.Colmenas.FindAsync(cDto.ColmenaId);
                if (colmena != null)
                {
                    int alzas = Math.Min(colmena.Alzas, cDto.AlzasCosechadas);
                    int medias = Math.Min(colmena.MediasAlzas, cDto.MediasAlzasCosechadas);
                    int tresCuartos = Math.Min(colmena.AlzasTresCuartos, cDto.AlzasTresCuartosCosechadas);

                    double kilos = (alzas * 22.0) + (tresCuartos * 17.0) + (medias * 12.0);
                    totalKilos += kilos;

                    // No se restan las alzas físicas, solo se descuenta la miel y se reduce el peso de la colmena correspondientemente
                    colmena.ProduccionMielKg = Math.Max(0, colmena.ProduccionMielKg - kilos);
                    colmena.PesoKg = Math.Max(0, colmena.PesoKg - kilos);
                    _context.Colmenas.Update(colmena);
                    
                    var nota = new NotaTecnica
                    {
                        ColmenaId = colmena.Id,
                        Fecha = DateTime.Now,
                        Detalles = string.IsNullOrWhiteSpace(dto.Notas) ? "Cosecha en masa registrada." : $"Cosecha en masa registrada: {dto.Notas}",
                        Temperatura = null,
                        Humedad = null,
                        EstadoReina = colmena.EstadoReina,
                        EstadoColmena = colmena.Estado,
                        AlzasCosechadas = alzas,
                        MediasAlzasCosechadas = medias,
                        AlzasTresCuartosCosechadas = tresCuartos,
                        KilosCosechados = kilos,
                        Extraccion = extraccion
                    };

                    _context.NotasTecnicas.Add(nota);
                }
            }

            extraccion.KilosTotales = totalKilos;

            if (totalKilos > 0 && analisisDestinoId.HasValue)
            {
                var ganancia = new Ganancia
                {
                    AnalisisId = analisisDestinoId.Value,
                    Titulo = "Venta de Miel - Extracción Masa",
                    Descripcion = "Ingreso automático por cosecha en masa (Cotiz. $40)",
                    Monto = totalKilos * GlobalSettings.PrecioMielActual * 40.0
                };
                _context.Ganancias.Add(ganancia);
                extraccion.Ganancia = ganancia;
            }

            _context.Extracciones.Add(extraccion);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Cosecha registrada con éxito." });
        }

        [HttpGet]
        public async Task<IActionResult> GetDetails(int id)
        {
            var extraccion = await _context.Extracciones
                .Include(e => e.Ganancia)
                .Include(e => e.NotasTecnicas)
                    .ThenInclude(n => n.Colmena)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (extraccion == null)
            {
                return Json(new { success = false, message = "Extracción no encontrada." });
            }

            var data = new
            {
                id = extraccion.Id,
                fecha = extraccion.Fecha.ToString("dd/MM/yyyy HH:mm"),
                kilosTotales = extraccion.KilosTotales,
                cantidadColmenasCosechadas = extraccion.CantidadColmenasCosechadas,
                notas = string.IsNullOrWhiteSpace(extraccion.Notas) ? "Sin observaciones adicionales." : extraccion.Notas,
                gananciaMonto = extraccion.Ganancia?.Monto,
                detallesColmenas = extraccion.NotasTecnicas.Select(n => new
                {
                    colmenaId = n.ColmenaId,
                    identificador = string.IsNullOrEmpty(n.Colmena?.Identificador) ? n.Colmena?.CodigoEscaneo : n.Colmena.Identificador,
                    alzas = n.AlzasCosechadas,
                    medias = n.MediasAlzasCosechadas,
                    trescuartos = n.AlzasTresCuartosCosechadas,
                    kilos = n.KilosCosechados
                }).ToList()
            };

            return Json(new { success = true, data = data });
        }
    }

    public class CosechaMasaDto
    {
        public List<CosechaColmenaDto> Colmenas { get; set; }
        public int? AnalisisId { get; set; }
        public bool CreateNewAnalisis { get; set; }
        public string Notas { get; set; }
    }

    public class CosechaColmenaDto
    {
        public int ColmenaId { get; set; }
        public int AlzasCosechadas { get; set; }
        public int MediasAlzasCosechadas { get; set; }
        public int AlzasTresCuartosCosechadas { get; set; }
    }
}
