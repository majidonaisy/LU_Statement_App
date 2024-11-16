using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StatementApplication.Data;
using StatementApplication.Models;
using System.Security.Claims;

namespace StatementApplication.Pages
{
    public class AdminLoginModel : PageModel
    {
        private readonly AppDataContext _context;

        public AdminLoginModel(AppDataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AdminBindingModel model { get; set; }

        //public PasswordHasher<Admin> _hasher = new PasswordHasher<Admin>();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var user = _context.Admins.FirstOrDefault(x => x.username == model.username);
            if (user == null)
            {
                return Page();
            }
            var correctPassword = /*_hasher.VerifyHashedPassword(user, user.password, model.password);*/ (user.password == model.password);
            if /*(correctPassword == PasswordVerificationResult.Failed)*/ (!correctPassword)
            {
                return Page();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName , user.username),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Remember across sessions
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToPage("/AdminDashboard");
        }
    }
}