using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StatementApplication.Data;
using StatementApplication.Models;
using StatementApplication.Services;

namespace StatementApplication.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardModel : PageModel
    {
        private readonly AppDataContext _context;
        EmailSender _sender;
        public List<Application> Applications { get; set; }
        public List<Employee> Employees { get; set; }
        public AdminDashboardModel(AppDataContext appDataContext, EmailSender emailSender)
        {
            _context = appDataContext;
            _sender = emailSender;
        }

        public void OnGet()
        {
            Applications = Applications = _context.Applications.Include(x => x.Statements)
           .Include(x => x.Student)
           .ToList();
            Employees = _context.Employees.ToList();
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

            return RedirectToPage();
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

            var application = Applications.FirstOrDefault(x => x.ApplicationId == id);
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
            await _sender.SendEmailAsync(email, "?? ?????? ????.", "?? ?????? ???? ????? ? ????? ????? ?? ??? ?????? ??? ??????");
            return RedirectToPage();
        }

    }
}
