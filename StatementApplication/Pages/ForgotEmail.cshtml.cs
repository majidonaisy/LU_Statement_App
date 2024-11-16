using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using StatementApplication.Data;

namespace StatementApplication.Pages
{
    public class ForgotEmailModel : PageModel
    {
        public AppDataContext _context { get; set; }
        [BindProperty]
        public int Id { get; set; }
        public string Message { get; set; } = "";
        public ForgotEmailModel(AppDataContext appDataContext)
        {
            _context = appDataContext;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost(int Id)
        {
            var user = _context.Students.FirstOrDefault(x => x.StudentId == Id);
            if (user == null)
            {
                Message = "No such ID Exists in our database";
                return Page();
            }
            else
            {
                var email = user.Email;
                if (email.IsNullOrEmpty())
                {
                    Message = "You have not entered an Email to this Id";
                    return Page();
                }
                int atIndex = email.IndexOf("@");
                if (atIndex < 0 || atIndex < 3)
                {
                    Message = "You have not entered an Email to this Id";
                    return Page();
                }
                string visiblePart = email.Substring(0, 3);
                string domain = email.Substring(atIndex);
                Message = "Your Email Is : " + visiblePart + "*****" + domain;
                return Page();
            }
        }
    }
}
