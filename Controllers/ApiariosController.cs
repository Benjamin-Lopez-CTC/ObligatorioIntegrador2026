using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligatorioIntegrador2026.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ObligatorioIntegrador2026.Controllers
{
    [Authorize]
    public class ApiariosController : Controller
    {
        private readonly AppDbContext _context;

        public ApiariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Apiarios
        public async Task<IActionResult> Index()
        {
            var apiarios = await _context.Apiarios
                .Include(a => a.Colmenas)
                .ToListAsync();
                
            return View(apiarios);
        }

        // GET: Apiarios/Detalles/5
        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiario = await _context.Apiarios
                .Include(a => a.Colmenas)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (apiario == null)
            {
                return NotFound();
            }

            return View(apiario);
        }

        // POST: Apiarios/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var apiario = await _context.Apiarios
                .Include(a => a.Colmenas)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (apiario != null)
            {
                _context.Colmenas.RemoveRange(apiario.Colmenas);
                _context.Apiarios.Remove(apiario);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"El apiario '{apiario.Nombre}' y sus colmenas fueron eliminados con éxito.";
            }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarNuevo(string Nombre, string UbicacionTexto, string StringIdentificador)
        {
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(UbicacionTexto))
            {
                return BadRequest("Faltan datos obligatorios.");
            }

            var apiario = new ObligatorioIntegrador2026.Models.Apiario
            {
                Nombre = Nombre,
                UbicacionTexto = UbicacionTexto,
                StringIdentificador = string.IsNullOrWhiteSpace(StringIdentificador) ? new System.Random().Next(100, 999).ToString() : StringIdentificador,
                FechaCreacion = System.DateTime.Now,
                UbicacionCoordenadas = "No registradas",
                Responsable = "N/A",
                NotasEstado = "Apiario recién registrado."
            };

            _context.Apiarios.Add(apiario);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"El apiario '{apiario.Nombre}' fue registrado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarNota(int id, string NotasEstado)
        {
            var apiario = await _context.Apiarios.FindAsync(id);
            if (apiario == null)
            {
                return NotFound();
            }

            apiario.NotasEstado = NotasEstado;
            apiario.UltimaEdicionNota = System.DateTime.Now;

            _context.Apiarios.Update(apiario);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "La nota de estado fue actualizada.";

            return RedirectToAction(nameof(Detalles), new { id = id });
        }
    }
}
