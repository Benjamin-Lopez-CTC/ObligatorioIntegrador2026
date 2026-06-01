using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligatorioIntegrador2026.Data;
using ObligatorioIntegrador2026.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioIntegrador2026.Controllers
{
    [Authorize]
    public class InformesController : Controller
    {
        private readonly AppDbContext _context;

        public InformesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var latestDec = await _context.Declaraciones
                .OrderByDescending(d => d.FechaEntrega)
                .FirstOrDefaultAsync();

            var model = new InformesViewModel();

            if (latestDec != null)
            {
                model.FechaUltimaDeclaracion = latestDec.FechaEntrega;
                model.FechaProximaDeclaracion = latestDec.FechaEntrega.AddYears(1);
                model.DiasRestantes = (int)Math.Ceiling((model.FechaProximaDeclaracion.Value - DateTime.Today).TotalDays);
            }
            else
            {
                model.FechaUltimaDeclaracion = null;
                model.FechaProximaDeclaracion = null;
                model.DiasRestantes = 0;
            }

            model.TotalColmenasPropiedad = await _context.Colmenas.CountAsync(c => !c.EsNucleo);
            model.TotalNucleos = await _context.Colmenas.CountAsync(c => c.EsNucleo);

            var apiarios = await _context.Apiarios
                .Include(a => a.Colmenas)
                .ToListAsync();

            model.Apiarios = apiarios.Select(a => new ApiarioInformeItem
            {
                Id = a.Id,
                Nro = a.StringIdentificador ?? "N/A",
                Nombre = a.Nombre,
                Departamento = a.Departamento ?? "No especificado",
                SeccionPolicial = a.SeccionPolicial ?? "No especificada",
                Paraje = a.Paraje ?? "No especificado",
                CantidadColmenas = a.Colmenas.Count(c => !c.EsNucleo),
                CantidadNucleos = a.Colmenas.Count(c => c.EsNucleo),
                Coordenadas = a.UbicacionCoordenadas ?? "No registradas"
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarFecha(DateTime fechaUltima)
        {
            if (fechaUltima == DateTime.MinValue)
            {
                TempData["ErrorMessage"] = "Fecha inválida.";
                return RedirectToAction(nameof(Index));
            }

            var latestDec = await _context.Declaraciones
                .OrderByDescending(d => d.FechaEntrega)
                .FirstOrDefaultAsync();

            if (latestDec != null)
            {
                latestDec.FechaEntrega = fechaUltima;
                _context.Declaraciones.Update(latestDec);
            }
            else
            {
                _context.Declaraciones.Add(new DeclaracionJurada { FechaEntrega = fechaUltima });
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Fecha de última declaración actualizada con éxito.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarEntregado()
        {
            var nuevaDec = new DeclaracionJurada { FechaEntrega = DateTime.Today };
            _context.Declaraciones.Add(nuevaDec);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "¡Informe marcado como entregado! Las fechas se actualizaron correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ExportarTxt()
        {
            var latestDec = await _context.Declaraciones
                .OrderByDescending(d => d.FechaEntrega)
                .FirstOrDefaultAsync();

            DateTime? fechaUltima = latestDec?.FechaEntrega;
            DateTime? fechaProxima = fechaUltima?.AddYears(1);

            int totalColmenas = await _context.Colmenas.CountAsync(c => !c.EsNucleo);
            int totalNucleos = await _context.Colmenas.CountAsync(c => c.EsNucleo);

            var apiarios = await _context.Apiarios
                .Include(a => a.Colmenas)
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("==================================================");
            sb.AppendLine("    REPORTE DE DECLARACIÓN JURADA - ZÁNGANOS S.A.");
            sb.AppendLine("==================================================");
            sb.AppendLine($"Fecha de Emisión: {DateTime.Now:dd/MM/yyyy HH:mm}");
            sb.AppendLine();
            sb.AppendLine("1. ESTADO DE DECLARACIONES JURADAS");
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine($"Última Declaración Presentada: {(fechaUltima.HasValue ? fechaUltima.Value.ToString("dd 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-ES")) : "No registrada")}");
            sb.AppendLine($"Próxima Declaración Pendiente: {(fechaProxima.HasValue ? fechaProxima.Value.ToString("dd 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-ES")) : "No registrada")}");
            sb.AppendLine();
            sb.AppendLine("2. STOCK APÍCOLA TOTAL");
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine($"Cantidad de colmenas en propiedad: {totalColmenas}");
            sb.AppendLine($"Cantidad de núcleos: {totalNucleos}");
            sb.AppendLine();
            sb.AppendLine("3. INFORME DETALLADO DE APIARIOS");
            sb.AppendLine("--------------------------------------------------");

            foreach (var a in apiarios)
            {
                sb.AppendLine($"[Apiario ID: {a.StringIdentificador ?? "N/A"}] {a.Nombre}");
                sb.AppendLine($"  - Departamento: {a.Departamento ?? "No especificado"}");
                sb.AppendLine($"  - Sección Policial: {a.SeccionPolicial ?? "No especificada"}");
                sb.AppendLine($"  - Paraje: {a.Paraje ?? "No especificado"}");
                sb.AppendLine($"  - Cantidad de Colmenas: {a.Colmenas.Count(c => !c.EsNucleo)}");
                sb.AppendLine($"  - Cantidad de núcleos: {a.Colmenas.Count(c => c.EsNucleo)}");
                sb.AppendLine($"  - Coordenadas: {a.UbicacionCoordenadas ?? "No registradas"}");
                sb.AppendLine("--------------------------------------------------");
            }

            string content = sb.ToString();
            byte[] fileBytes = Encoding.UTF8.GetBytes(content);
            return File(fileBytes, "text/plain", $"declaracion_jurada_sinatpa_{DateTime.Now:yyyyMMdd}.txt");
        }
    }
}
