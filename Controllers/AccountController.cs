using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligatorioIntegrador2026.Data;
using ObligatorioIntegrador2026.ViewModels;
using System.Security.Claims;

namespace ObligatorioIntegrador2026.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    await RecordLoginAttempt(model.Username, true);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim("FullName", user.FullName),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }

                await RecordLoginAttempt(model.Username, false);
                ModelState.AddModelError(string.Empty, "Usuario o contraseña inválidos");
            }

            return View(model);
        }

        private async Task RecordLoginAttempt(string username, bool isSuccess)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Desconocida";
            var userAgent = Request.Headers["User-Agent"].ToString();
            userAgent = userAgent.Length > 200 ? userAgent.Substring(0, 200) : userAgent;

            string location = "Desconocida";
            if (ip == "::1" || ip == "127.0.0.1")
            {
                location = "Localhost";
            }
            else if (ip != "Desconocida")
            {
                try
                {
                    using var httpClient = new HttpClient();
                    // Timeout corto para no bloquear el login si la API falla
                    httpClient.Timeout = TimeSpan.FromSeconds(3);
                    var response = await httpClient.GetAsync($"http://ip-api.com/json/{ip}");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        using var doc = System.Text.Json.JsonDocument.Parse(json);
                        var status = doc.RootElement.GetProperty("status").GetString();
                        if (status == "success")
                        {
                            var city = doc.RootElement.GetProperty("city").GetString();
                            var country = doc.RootElement.GetProperty("countryCode").GetString();
                            location = $"{city}, {country}";
                        }
                    }
                }
                catch
                {
                    location = ip; // Fallback
                }
            }

            var record = new ObligatorioIntegrador2026.Models.LoginRecord
            {
                AttemptDate = DateTime.Now,
                Username = username ?? "Desconocido",
                IpAddress = ip,
                DeviceBrowser = string.IsNullOrEmpty(userAgent) ? "Desconocido" : userAgent,
                Location = location,
                IsSuccess = isSuccess
            };

            _context.LoginRecords.Add(record);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
