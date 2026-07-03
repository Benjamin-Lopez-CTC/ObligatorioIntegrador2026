using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligatorioIntegrador2026.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ObligatorioIntegrador2026.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> RegistroLogins(string filter = "all", string? search = null)
        {
            var today = DateTime.Today;
            var yesterday = DateTime.Now.AddDays(-1);
            
            var query = _context.LoginRecords.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(r => EF.Functions.Like(r.Username, $"%{search}%"));
            }

            if (filter == "24h")
            {
                query = query.Where(r => r.AttemptDate >= yesterday);
            }
            else if (filter == "failed")
            {
                query = query.Where(r => !r.IsSuccess);
            }

            var filteredRecords = await query
                .OrderByDescending(r => r.AttemptDate)
                .ToListAsync();

            // Stats are always calculated based on all records to show global metrics
            var totalStatsQuery = await _context.LoginRecords.ToListAsync();

            ViewBag.TotalHoy = totalStatsQuery.Count(r => r.AttemptDate.Date == today);
            ViewBag.Fallidos = totalStatsQuery.Count(r => !r.IsSuccess);

            ViewBag.CurrentFilter = filter;
            ViewBag.CurrentSearch = search;

            // Formatear los registros para mostrar solo la información amigable del User-Agent
            foreach (var record in filteredRecords)
            {
                record.DeviceBrowser = ParseUserAgent(record.DeviceBrowser);
            }

            return View(filteredRecords);
        }

        private string ParseUserAgent(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                return "Desconocido";

            string os = "Desconocido";
            string browser = "Desconocido";

            // Detectar Sistema Operativo
            if (userAgent.Contains("Windows", StringComparison.OrdinalIgnoreCase))
            {
                os = "Windows";
            }
            else if (userAgent.Contains("Android", StringComparison.OrdinalIgnoreCase))
            {
                os = "Android";
            }
            else if (userAgent.Contains("iPhone", StringComparison.OrdinalIgnoreCase))
            {
                os = "iPhone";
            }
            else if (userAgent.Contains("iPad", StringComparison.OrdinalIgnoreCase))
            {
                os = "iPad";
            }
            else if (userAgent.Contains("Macintosh", StringComparison.OrdinalIgnoreCase) || userAgent.Contains("Mac OS X", StringComparison.OrdinalIgnoreCase))
            {
                os = "macOS";
            }
            else if (userAgent.Contains("Linux", StringComparison.OrdinalIgnoreCase))
            {
                os = "Linux";
            }

            // Detectar Navegador (el orden importa para navegadores basados en Chromium)
            if (userAgent.Contains("Edg/", StringComparison.OrdinalIgnoreCase) || userAgent.Contains("Edge", StringComparison.OrdinalIgnoreCase))
            {
                browser = "Edge";
            }
            else if (userAgent.Contains("OPR/", StringComparison.OrdinalIgnoreCase) || userAgent.Contains("Opera", StringComparison.OrdinalIgnoreCase))
            {
                browser = "Opera";
            }
            else if (userAgent.Contains("Chrome", StringComparison.OrdinalIgnoreCase))
            {
                browser = "Chrome";
            }
            else if (userAgent.Contains("Firefox", StringComparison.OrdinalIgnoreCase))
            {
                browser = "Firefox";
            }
            else if (userAgent.Contains("Safari", StringComparison.OrdinalIgnoreCase))
            {
                browser = "Safari";
            }

            if (os == "Desconocido" && browser == "Desconocido")
            {
                // Fallback si no detectamos nada común: recortamos el User-Agent original
                return userAgent.Length > 50 ? userAgent.Substring(0, 50) + "..." : userAgent;
            }

            return $"{browser} en {os}";
        }
    }
}
