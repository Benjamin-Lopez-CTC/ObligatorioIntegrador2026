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

        public IActionResult Movimientos()
        {
            return View();
        }

        public async Task<IActionResult> CompararApiarios()
        {
            var apiarios = await _context.Apiarios
                .Include(a => a.Colmenas)
                .ToListAsync();
            return View(apiarios);
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
