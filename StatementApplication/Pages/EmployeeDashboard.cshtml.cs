using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StatementApplication.Data;
using StatementApplication.Models;
using StatementApplication.Services;
using System.Security.Claims;

namespace StatementApplication.Pages
{
    [Authorize(Roles = "Employee")]
    public class EmployeeDashboardModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int? StudentId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? ApplicationId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }






        private readonly AppDataContext _context;
        EmailSender _sender;
        public string username { get; set; }
        
        public EmployeeDashboardModel(AppDataContext appDataContext , EmailSender emailSender)
        {
            _context = appDataContext;
            _sender = emailSender;
        }
        
        public List<Application> Applications { get; set; }
        public void OnGet(int? StudentId , int? ApplicationId, DateTime? StartDate )
        {
            var query = _context.Applications.AsQueryable();

            if (StudentId.HasValue)
            {
                query = query.Where(a => a.StudentId == StudentId);
            }

            if (ApplicationId.HasValue)
            {
                query = query.Where(a => a.ApplicationId == ApplicationId);
            }

            if (StartDate.HasValue)
            {
                query = query.Where(a => a.SubmissionDate >= StartDate.Value);
            }
            if (!string.IsNullOrEmpty(Status))
            {
                if (Status == "Handled")
                {
                    query = query.Where(s => s.Status == "Handled"); // Assuming 'Done' means handled
                }
                else if (Status == "Unhandled")
                {
                    query = query.Where(s => s.Status != "Handled"); // Everything that isn't done
                }
            }
            username = User.FindFirstValue(ClaimTypes.GivenName);
            Applications = query.Include(x=>x.Statements)
                .Include(x=>x.Student)
                .ToList();

        }
        public async Task<IActionResult> OnPostMarkDone(int id)
        {
            var statement = await _context.Statement.FindAsync(id);
            if (statement == null)
            {
                return NotFound();
            }
            var studentid = statement.StudentId;
            var student = _context.Students.FirstOrDefault(x => x.StudentId == studentid);
            // Mark the statement as done
            statement.Status = "Done";
            
            await _context.SaveChangesAsync();
            
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostMarkDenied(int id)
        {
            var statement = await _context.Statement.FindAsync(id);
            if (statement == null)
            {
                return NotFound();
            }
            statement.Status = "Denied";
            await _context.SaveChangesAsync();

            return Page();
        }
        public async Task<IActionResult> OnPostMarkReceived(int id)
        {
            var statement = await _context.Statement.FindAsync(id);
            if (statement == null)
            {
                return NotFound();
            }
            var studentid = statement.StudentId;
            var student = _context.Students.FirstOrDefault(x => x.StudentId == studentid);
            var email = student.Email;
            var type = statement.Type;
            // Mark the statement as done
            statement.Status = "Received";
            
            await _context.SaveChangesAsync();
            
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostSubmitApplication(int id)
        {
            Applications = _context.Applications.Include(x => x.Statements)
           .Include(x => x.Student)
           .ToList();

            var application = Applications.FirstOrDefault(x=>x.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }
            foreach (var statement in application.Statements)
            {
                if (statement.Status == "Pending")
                {
                    return RedirectToPage();
                }
            }
            var studentid = application.StudentId;
            var student = _context.Students.FirstOrDefault(x => x.StudentId == studentid);
            var email = student.Email;

            application.Status = "Handled";

            await _context.SaveChangesAsync();
            await _sender.SendEmailAsync(email, "تم معالجة طلبك.", "تم معالجة طلبك بنجاح و يمكنك اتحقق من وضع افداتك على الموقع");
            return RedirectToPage();
        }
    }
}
