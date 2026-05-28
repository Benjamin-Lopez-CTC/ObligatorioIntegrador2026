using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ObligatorioIntegrador2026.Controllers
{
    [Authorize]
    public class MasController : Controller
    {
        public IActionResult Movimientos()
        {
            return View();
        }

        public IActionResult CompararApiarios()
        {
            return View();
        }

        public IActionResult CompararColmenas()
        {
            return View();
        }

        public IActionResult Finanzacion()
        {
            return View();
        }

        public IActionResult Inventario()
        {
            return View();
        }
    }
}
