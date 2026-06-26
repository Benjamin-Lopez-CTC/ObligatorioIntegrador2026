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

            return View(filteredRecords);
        }
    }
}
