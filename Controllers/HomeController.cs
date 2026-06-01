using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligatorioIntegrador2026.Data;
using ObligatorioIntegrador2026.Models;

namespace ObligatorioIntegrador2026.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HomeController(ILogger<HomeController> logger, AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel();

        model.TotalApiarios = await _context.Apiarios.CountAsync();
        model.TotalColmenas = await _context.Colmenas.CountAsync();
        model.TotalProduccionMielKg = await _context.Colmenas.SumAsync(c => (double?)c.ProduccionMielKg) ?? 0;
        
        model.ColmenasEnAlerta = await _context.Colmenas
            .Include(c => c.Apiario)
            .Where(c => c.Estado == "Alerta" || c.Estado == "Crítico")
            .OrderByDescending(c => c.Id)
            .Take(10)
            .ToListAsync();
            
        model.AlertasActivas = await _context.Colmenas.CountAsync(c => c.Estado == "Alerta" || c.Estado == "Crítico");

        model.MovimientosVigentes = await _context.Movimientos
            .Include(m => m.Colmena)
            .Include(m => m.ApiarioOrigen)
            .Include(m => m.ApiarioDestino)
            .Where(m => m.Estado == "Vigente")
            .OrderBy(m => m.FechaRegreso)
            .Take(5)
            .ToListAsync();

        model.InventarioBajoStock = await _context.Equipments
            .Where(e => e.Stock <= e.LowThreshold)
            .OrderBy(e => e.Stock)
            .Take(5)
            .ToListAsync();

        // Calcular la tendencia de producción de los últimos 6 meses en base a la producción actual
        var tendencia = new List<ProduccionMensual>();
        var hoy = DateTime.Now;
        var mesesAbreviados = new[] { "", "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };
        var perfilProduccion = new[] { 0, 0.30, 0.20, 0.15, 0.05, 0.02, 0.01, 0.01, 0.01, 0.05, 0.10, 0.15, 0.25 };
        
        var ultimosMeses = new List<DateTime>();
        for (int i = 5; i >= 0; i--)
        {
            ultimosMeses.Add(hoy.AddMonths(-i));
        }

        double sumaPesos = 0;
        foreach (var mesFecha in ultimosMeses)
        {
            sumaPesos += perfilProduccion[mesFecha.Month];
        }

        foreach (var mesFecha in ultimosMeses)
        {
            double pesoMes = perfilProduccion[mesFecha.Month];
            double cantKg = sumaPesos > 0 ? model.TotalProduccionMielKg * (pesoMes / sumaPesos) : 0;
            
            tendencia.Add(new ProduccionMensual
            {
                Mes = mesesAbreviados[mesFecha.Month],
                CantidadKg = cantKg,
                EsMesActual = (mesFecha.Month == hoy.Month && mesFecha.Year == hoy.Year)
            });
        }

        double maxMielMes = tendencia.Any() ? tendencia.Max(t => t.CantidadKg) : 0;
        foreach (var item in tendencia)
        {
            item.PorcentajeAltura = maxMielMes > 0 ? (item.CantidadKg / maxMielMes) * 90 + 10 : 10;
        }

        model.TendenciaProduccion = tendencia;

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DescargarReporte()
    {
        var totalApiarios = await _context.Apiarios.CountAsync();
        var totalColmenas = await _context.Colmenas.CountAsync();
        var totalMiel = await _context.Colmenas.SumAsync(c => (double?)c.ProduccionMielKg) ?? 0;
        var totalAlertas = await _context.Colmenas.CountAsync(c => c.Estado == "Alerta" || c.Estado == "Crítico");

        var apiarios = await _context.Apiarios.ToListAsync();
        
        var colmenasEnAlerta = await _context.Colmenas
            .Include(c => c.Apiario)
            .Where(c => c.Estado == "Alerta" || c.Estado == "Crítico")
            .OrderByDescending(c => c.Estado == "Crítico")
            .ThenByDescending(c => c.Id)
            .ToListAsync();

        var movimientos = await _context.Movimientos
            .Include(m => m.Colmena)
            .Include(m => m.ApiarioOrigen)
            .Include(m => m.ApiarioDestino)
            .Where(m => m.Estado == "Vigente")
            .OrderBy(m => m.FechaRegreso)
            .ToListAsync();

        var inventarioBajoStock = await _context.Equipments
            .Where(e => e.Stock <= e.LowThreshold)
            .OrderBy(e => e.Stock)
            .ToListAsync();

        var logoPath = Path.Combine(_webHostEnvironment.WebRootPath, "Logo.png");

        var pdfBytes = ObligatorioIntegrador2026.Services.PdfReportGenerator.GenerateGlobalReport(
            totalApiarios, totalColmenas, totalMiel, totalAlertas,
            apiarios, colmenasEnAlerta, movimientos, inventarioBajoStock,
            logoPath
        );

        return File(pdfBytes, "application/pdf", $"ReporteGlobal_Zanganos_{DateTime.Now:yyyyMMdd}.pdf");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
