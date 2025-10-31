using BusinessLayer.DTO;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;

namespace HEMS_NamNMSE182871.Pages.Account;

public class LoginModel : PageModel
{
    private readonly AuthService _authService;

    public LoginModel(AuthService authService)
    {
        _authService = authService;
    }

    [BindProperty]
    public AccountLoginDto Input { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var account = await _authService.LoginAsync(Input.EmailAddress, Input.StoreAccountPassword);
        
        if (account == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password");
            return Page();
        }

        // Store in TempData
        TempData["Account"] = JsonSerializer.Serialize(account);

        // Create claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, account.EmailAddress),
            new Claim(ClaimTypes.NameIdentifier, account.StoreAccountID.ToString()),
            new Claim("FullName", account.FullName),
            new Claim("Role", account.StoreAccountRole.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToPage("/Equipment/Index");
    }
}

