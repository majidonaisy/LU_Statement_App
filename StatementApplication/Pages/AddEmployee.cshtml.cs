using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StatementApplication.Data;
using StatementApplication.Models;

namespace StatementApplication.Pages
{
    [Authorize(Roles = "Admin")]
    public class AddEmployeeModel : PageModel
    {
        private readonly PasswordHasher<Employee> _passwordHasher = new PasswordHasher<Employee>();
        public AppDataContext _context;
        [BindProperty]
        public EmployeeBingingModel model { get; set; }

        public AddEmployeeModel(AppDataContext appDataContext)
        {
            _context = appDataContext;
        }
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            var user = _context.Employees.ToList().FirstOrDefault(x=>x.username == model.username);
            if (user == null)
            {
                var newEmployee = new Employee{ username = model.username};
                var newPassword = _passwordHasher.HashPassword(newEmployee, model.password);
                newEmployee.password = newPassword;
                _context.Employees.Add(newEmployee);
                _context.SaveChanges();
            }
        }

    }
}
