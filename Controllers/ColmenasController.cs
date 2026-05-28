using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ObligatorioIntegrador2026.Controllers
{
    [Authorize]
    public class ColmenasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
