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
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginBindingModel model { get; set; }
        public PasswordHasher<Student> _hasher = new PasswordHasher<Student>(); 
        private readonly AppDataContext _context;
        public LoginModel( AppDataContext appDataContext)
        {
            _context = appDataContext;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            var user = _context.Students.FirstOrDefault(x=>x.Email == model.Email);
            if (user == null)
            {
                return Page();
            }
            var correctPassword = _hasher.VerifyHashedPassword(user, user.Password, model.Password);
            if (correctPassword == PasswordVerificationResult.Failed)
            {
                return Page();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.GivenName , user.Name),
                new Claim(ClaimTypes.Role, "Student"),
                new Claim(ClaimTypes.Sid , user.StudentId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Remember across sessions
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),authProperties);
            
            // Redirect to the home page or a secure page
            return RedirectToPage("/apply");
        }
    }
}
