using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StatementApplication.Data;
using StatementApplication.Models;
using System.Security.Claims;

namespace StatementApplication.Pages
{
    [Authorize(Roles ="Student")]
    public class AllStatementsModel : PageModel
    {
        private readonly AppDataContext _context;
        public List<Application> Applications { get; set; }
        public int StudentId {  get; set; }
        public AllStatementsModel(AppDataContext appDataContext)
        {
            _context = appDataContext;
        }

        public void OnGet()
        {
            
            StudentId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            Applications = _context.Applications.Where(x=>x.StudentId == StudentId).Include(a => a.Student)
                .Include(a => a.Statements)
                .ToList();
                
                
        }
    }
}
