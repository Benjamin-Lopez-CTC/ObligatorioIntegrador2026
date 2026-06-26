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
            .Where(m => m.Estado == "Vigente" && m.FechaRegreso < DateTime.Now)
            .OrderBy(m => m.FechaRegreso)
            .Take(5)
            .ToListAsync();

        model.InventarioBajoStock = await _context.Equipments
            .Where(e => e.Stock <= e.LowThreshold)
            .OrderBy(e => e.Stock)
            .Take(5)
            .ToListAsync();

        // Query active financial analysis
        var activeAnalysis = await _context.Analisis
            .Include(a => a.Inversiones)
            .Include(a => a.Ganancias)
            .FirstOrDefaultAsync(a => a.FechaFin == null);

        if (activeAnalysis != null)
        {
            model.HasActiveAnalysis = true;
            model.ActiveTotalInversion = activeAnalysis.TotalInversion;
            model.ActiveGananciaBruta = activeAnalysis.GananciaBruta;
            model.ActiveBalanceNeto = activeAnalysis.BalanceNeto;
            model.ActiveEgresosCount = activeAnalysis.Inversiones?.Count ?? 0;
            model.ActiveIngresosCount = activeAnalysis.Ganancias?.Count ?? 0;
        }
        else
        {
            model.HasActiveAnalysis = false;
        }

        // Load latest declaration dates
        var latestDec = await _context.Declaraciones
            .OrderByDescending(d => d.FechaEntrega)
            .FirstOrDefaultAsync();

        if (latestDec != null)
        {
            model.FechaUltimaDeclaracion = latestDec.FechaEntrega;
            model.FechaProximaDeclaracion = latestDec.FechaEntrega.AddYears(1);
        }

        // Calcular la tendencia de producción de los últimos 6 meses en base a las extracciones
        var tendencia = new List<ProduccionMensual>();
        var hoy = DateTime.Now;
        var mesesAbreviados = new[] { "", "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };
        
        // Fetch extracciones from the last 5 months + current month
        var fechaLimite = hoy.AddMonths(-5).AddDays(-hoy.Day + 1).Date; // Start of the 6th month back
        var extracciones = await _context.Extracciones
            .Where(e => e.Fecha >= fechaLimite)
            .ToListAsync();
        
        var ultimosMeses = new List<DateTime>();
        for (int i = 5; i >= 0; i--)
        {
            ultimosMeses.Add(hoy.AddMonths(-i));
        }

        foreach (var mesFecha in ultimosMeses)
        {
            double cantKg = extracciones
                .Where(e => e.Fecha.Month == mesFecha.Month && e.Fecha.Year == mesFecha.Year)
                .Sum(e => e.KilosTotales);
            
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
            .Where(m => m.Estado == "Vigente" && m.FechaRegreso < DateTime.Now)
            .OrderBy(m => m.FechaRegreso)
            .ToListAsync();

        var inventarioBajoStock = await _context.Equipments
            .Where(e => e.Stock <= e.LowThreshold)
            .OrderBy(e => e.Stock)
            .ToListAsync();

        var logoPath = Path.Combine(_webHostEnvironment.WebRootPath, "Logo.png");

        var activeAnalysis = await _context.Analisis
            .Include(a => a.Inversiones)
            .Include(a => a.Ganancias)
            .FirstOrDefaultAsync(a => a.FechaFin == null);

        bool hasActiveAnalysis = activeAnalysis != null;
        double activeTotalInversion = activeAnalysis?.TotalInversion ?? 0;
        double activeGananciaBruta = activeAnalysis?.GananciaBruta ?? 0;
        double activeBalanceNeto = activeAnalysis?.BalanceNeto ?? 0;

        // Load latest declaration dates
        var latestDec = await _context.Declaraciones
            .OrderByDescending(d => d.FechaEntrega)
            .FirstOrDefaultAsync();
        DateTime? fechaUltima = latestDec?.FechaEntrega;
        DateTime? fechaProxima = fechaUltima?.AddYears(1);

        var pdfBytes = ObligatorioIntegrador2026.Services.PdfReportGenerator.GenerateGlobalReport(
            totalApiarios, totalColmenas, totalMiel, totalAlertas,
            apiarios, colmenasEnAlerta, movimientos, inventarioBajoStock,
            hasActiveAnalysis, activeTotalInversion, activeGananciaBruta, activeBalanceNeto,
            fechaUltima, fechaProxima,
            logoPath
        );

        return File(pdfBytes, "application/pdf", $"ReporteGlobal_Zanganos_{DateTime.Now:yyyyMMdd}.pdf");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOfflineUrls()
    {
        var urls = new List<string>
        {
            "/Colmenas",
            "/Apiarios"
        };

        var colmenas = await _context.Colmenas.Select(c => c.Id).ToListAsync();
        foreach (var id in colmenas)
        {
            urls.Add($"/Colmenas/Detalles/{id}");
        }

        var apiarios = await _context.Apiarios.Select(a => a.Id).ToListAsync();
        foreach (var id in apiarios)
        {
            urls.Add($"/Apiarios/Detalles/{id}");
        }

        return Json(urls);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
