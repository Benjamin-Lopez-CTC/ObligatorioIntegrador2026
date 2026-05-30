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
        public async Task<IActionResult> RegistrarNueva([Bind("Identificador,ApiarioId,CantidadAbejas,UbicacionIntraApiario,EstadoReina,ComportamientoAbejas,EsPiloto")] Colmena colmena)
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
                return RedirectToAction(nameof(Index));
            }
            
            TempData["ErrorMessage"] = "Error al registrar la colmena. Revise los datos e intente nuevamente.";
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
            var colmena = await _context.Colmenas.Include(c => c.Apiario).FirstOrDefaultAsync(c => c.Id == id);
            if (colmena == null) return NotFound();

            ActualizarEstadoAutomatico(colmena);
            await _context.SaveChangesAsync();

            return View(colmena);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarColmena(int id, double? temperatura, double? humedad, double peso, double produccion, string comportamiento)
        {
            var colmena = await _context.Colmenas.FindAsync(id);
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
            await _context.SaveChangesAsync();
            
            TempData["SuccessMessage"] = "Colmena actualizada exitosamente.";
            return RedirectToAction(nameof(Detalles), new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AñadirNotaTecnica(int id, string detalles, string estadoReina, string estadoColmena)
        {
            var colmena = await _context.Colmenas.FindAsync(id);
            if (colmena == null) return NotFound();

            colmena.UltimaNotaTecnica = detalles;
            colmena.FechaUltimaNota = DateTime.Now;
            colmena.EstadoReina = estadoReina;
            
            // Allow manual override of state unless automatic rules force it to Alert
            colmena.Estado = estadoColmena;

            ActualizarEstadoAutomatico(colmena);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Nota técnica agregada correctamente.";
            return RedirectToAction(nameof(Detalles), new { id = id });
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

            if (colmena.FechaUltimaNota.HasValue)
            {
                if ((DateTime.Now - colmena.FechaUltimaNota.Value).TotalDays > 30) alertReason = true;
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
}
