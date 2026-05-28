using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ObligatorioIntegrador2026.Controllers
{
    [Authorize]
    public class ApiariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
