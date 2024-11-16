using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StatementApplication.Data.Enums;
using StatementApplication.Extensions;

namespace StatementApplication.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        LMDType Type = LMDType.افادة_انهاء_بالعلامات_فرنسي;
       
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Login");
        }
        public void OnGet()
        {
            
        }
    }
}
