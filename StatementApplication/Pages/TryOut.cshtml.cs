using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StatementApplication.Services;
using System.Security.Claims;

namespace StatementApplication.Pages
{
    public class TryOutModel : PageModel
    {
        public string Role {  get; set; }
        public string email { get; set; }
        public string id { get; set; }
        public int intid { get; set; }
        public List<Claim> claims { get; set; }

        public IActionResult OnGet()
        {
            var user = HttpContext.User;
            claims  = user.Claims.ToList();
            return Page();

        }
    }
}
