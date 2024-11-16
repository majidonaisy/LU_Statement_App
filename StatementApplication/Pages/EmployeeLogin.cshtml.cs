using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using StatementApplication.Data;
using StatementApplication.Models;

namespace StatementApplication.Pages
{
    public class EmployeeLoginModel : PageModel
    {
        private readonly AppDataContext _context;
        public PasswordHasher<Employee> _hasher = new PasswordHasher<Employee>();

        [BindProperty]
        public EmployeeBingingModel model { get; set; }
        public EmployeeLoginModel(AppDataContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost() 
        {
            var user = _context.Employees.FirstOrDefault(x => x.username == model.username);
            if (user == null)
            {
                return Page();
            }
            var correctPassword = _hasher.VerifyHashedPassword(user, user.password, model.password);
            if (correctPassword == PasswordVerificationResult.Failed)
            {
                return Page();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName , user.username),
                new Claim(ClaimTypes.Role, "Employee"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Remember across sessions
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToPage("/EmployeeDashboard");
        }
    }
}
