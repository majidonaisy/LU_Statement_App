using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StatementApplication.Data;
using StatementApplication.Data.Enums;
using StatementApplication.Extensions;
using StatementApplication.Models;
using System.Security.Claims;

namespace StatementApplication.Pages
{
    [Authorize(Roles = "Student")]
    [Authorize(Policy = "VerifiedStudent")]
    public class ApplyModel : PageModel
    {
        public AppDataContext _context;
        public ApplyModel(AppDataContext context)
        {
            _context = context;
        }
        public int UserId { get; set; } 
        public City City { get; set; }
        public LMDType LMDType { get; set; }
        [BindProperty]
        public StatementApplicationModel model { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> LMDTypes { get; set; }
        public List<SelectListItem> MasterTypes { get; set; }

        public void OnGet()
        {
            Cities = Enum.GetValues(typeof(City)).Cast<City>().Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = e.ToString()
            }).ToList();

            LMDTypes = Enum.GetValues(typeof(LMDType)).Cast<LMDType>().Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = e.GetDisplayName()
            }).ToList();
            MasterTypes = Enum.GetValues(typeof(MasterType)).Cast<MasterType>().Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = e.GetDisplayName(),
            }).ToList();
            UserId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));

        }
        public IActionResult OnPost() 
        {
            UserId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            Application newApplication = new Application
            {
                Statements = new List<Statement>(),
                SubmissionDate = DateTime.Now,
                StudentId = UserId
            };
            
            
            foreach(var type in model.Type)
            {
                var statement = new Statement
                {
                    Type = type,
                    FirstNameAra = model.FirstNameAra,
                    MiddleNameAra = model.MiddleNameAra,
                    LastNameAra = model.LastNameAra,
                    FirstNameEng = model.FirstNameEng,
                    MiddleNameEng = model.MiddleNameEng,
                    LastNameEng = model.LastNameEng,
                    DOB = model.DOB,
                    POB = model.POB,
                    Phonenumber = model.Phonenumber,
                    StudentId = UserId,
                    SubmitionDate = DateTime.Now,
                    Status = "Pending",
                };
                
                _context.Statement.Add(statement);
                newApplication.Statements.Add(statement);
                
            }
            _context.Applications.Add(newApplication);
            _context.SaveChanges();
            return RedirectToPage("/Apply");
        }
    }
}
