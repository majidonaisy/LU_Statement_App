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
        
    }
}
