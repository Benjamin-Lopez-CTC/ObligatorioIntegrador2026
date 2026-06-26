using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using QuestPDF.Infrastructure;
using ObligatorioIntegrador2026.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar licencia de QuestPDF
QuestPDF.Settings.License = LicenseType.Community;

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new ObligatorioIntegrador2026.ModelBinders.InvariantDecimalModelBinderProvider());
});
builder.Services.AddHttpClient();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(12);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Create DB if not exists
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    bool tableExists = false;
    try
    {
        _ = context.Equipments.FirstOrDefault();
        _ = context.Apiarios.FirstOrDefault();
        _ = context.Colmenas.FirstOrDefault();
        _ = context.Treatments.FirstOrDefault();
        _ = context.NotasTecnicas.FirstOrDefault();
        _ = context.Movimientos.FirstOrDefault();
        _ = context.Analisis.FirstOrDefault();
        _ = context.Inversiones.FirstOrDefault();
        _ = context.Ganancias.FirstOrDefault();
        _ = context.Declaraciones.FirstOrDefault();
        tableExists = true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Equipments or Apiarios table does not exist or table check failed: {ex.Message}");
        tableExists = false;
    }

    if (!tableExists)
    {
        try
        {
            Console.WriteLine("Closing connection and calling EnsureDeleted...");
            context.Database.CloseConnection();
            context.Database.EnsureDeleted();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during EnsureDeleted: {ex.Message}");
            try
            {
                if (System.IO.File.Exists("ZanganosSA.db"))
                {
                    Console.WriteLine("File ZanganosSA.db exists. Attempting manual deletion...");
                    System.IO.File.Delete("ZanganosSA.db");
                    Console.WriteLine("Manual deletion succeeded.");
                }
            }
            catch (Exception ex2)
            {
                Console.WriteLine($"Error during manual file deletion: {ex2.Message}");
            }
        }

        try
        {
            Console.WriteLine("Calling EnsureCreated...");
            context.Database.EnsureCreated();
            Console.WriteLine("EnsureCreated completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during EnsureCreated: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }

    // Fix existing future inspection dates if any
    try
    {
        var futureApiarios = context.Apiarios.Where(a => a.UltimaInspeccion > DateTime.Now).ToList();
        if (futureApiarios.Any())
        {
            Console.WriteLine($"[DB SETUP] Encontrados {futureApiarios.Count} apiarios con fecha de inspección futura. Corrigiendo...");
            foreach (var apiario in futureApiarios)
            {
                if (apiario.UltimaInspeccion.HasValue)
                {
                    var oldDate = apiario.UltimaInspeccion.Value;
                    // Cambiar el mes de octubre (10) a mayo (5)
                    apiario.UltimaInspeccion = new DateTime(2026, 5, Math.Min(oldDate.Day, 31), oldDate.Hour, oldDate.Minute, oldDate.Second);
                    Console.WriteLine($"[DB SETUP] Apiario {apiario.Id} ({apiario.Nombre}): {oldDate:yyyy-MM-dd} -> {apiario.UltimaInspeccion.Value:yyyy-MM-dd}");
                }
            }
            context.SaveChanges();
            Console.WriteLine("[DB SETUP] Corrección de fechas completada con éxito.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[DB SETUP] Error al corregir fechas de inspección futuras: {ex.Message}");
    }

    // Fix "Vergues" spelling in existing database
    try
    {
        var userVergues = context.Users.FirstOrDefault(u => u.Username == "m.vergues");
        if (userVergues != null)
        {
            Console.WriteLine("[DB SETUP] Corrigiendo usuario m.vergues -> m.verges...");
            userVergues.Username = "m.verges";
            userVergues.FullName = "Matías Verges";
            context.Users.Update(userVergues);
        }

        var apiariosVergues = context.Apiarios.Where(a => a.Responsable == "Matías Vergues").ToList();
        if (apiariosVergues.Any())
        {
            Console.WriteLine($"[DB SETUP] Corrigiendo responsable en {apiariosVergues.Count} apiarios...");
            foreach (var apiario in apiariosVergues)
            {
                apiario.Responsable = "Matías Verges";
                context.Apiarios.Update(apiario);
            }
        }
        context.SaveChanges();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[DB SETUP] Error al corregir ortografía Vergues: {ex.Message}");
    }

    // Proactive password hashing update on database setup
    try
    {
        var users = context.Users.ToList();
        var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<ObligatorioIntegrador2026.Models.User>();
        bool anyChanged = false;
        foreach (var u in users)
        {
            if (!u.Password.StartsWith("AQAAAA"))
            {
                u.Password = hasher.HashPassword(u, u.Password);
                context.Users.Update(u);
                anyChanged = true;
                Console.WriteLine($"[SECURITY] Migrando contraseña de usuario '{u.Username}' a hash seguro...");
            }
        }
        if (anyChanged)
        {
            context.SaveChanges();
            Console.WriteLine("[SECURITY] Migración de contraseñas a hashes segura completada.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[SECURITY ERROR] Error al migrar contraseñas existentes: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Print seeded data check
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    try
    {
        var count = context.Equipments.Count();
        Console.WriteLine($"DEBUG DB: Number of equipments in DB: {count}");
        foreach (var eq in context.Equipments)
        {
            Console.WriteLine($"DEBUG DB: - {eq.Id}: {eq.Name} (Stock: {eq.Stock}, Category: {eq.Category})");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"DEBUG DB: Failed to query equipments count: {ex.Message}");
    }
}

app.Run();
