using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using ObligatorioIntegrador2026.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
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
