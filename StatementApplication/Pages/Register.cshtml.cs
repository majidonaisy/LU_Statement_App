using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StatementApplication.Configs;
using StatementApplication.Data;
using StatementApplication.Models;
using StatementApplication.Services;

namespace StatementApplication.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly PasswordHasher<Student> _passwordHasher = new PasswordHasher<Student>();
        AppDataContext _context;
        EmailSender _sender;
        EmailVerificationTokenMaker _tokenGenerator;
        SecretKey _secretKey;

        public RegisterModel( AppDataContext context, EmailSender emailSender,EmailVerificationTokenMaker tokenGenerator,SecretKey secret) 
        {
            _context = context;
            _sender = emailSender;
            _tokenGenerator = tokenGenerator;
            _secretKey = secret;

        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(RegisterBindingModel model)
        {
            var exists = _context.Students.FirstOrDefault(x => x.StudentId == model.Id) ;
            if (exists != null)
            {
                _context.Students.FirstOrDefault(x => x.Email == model.Email);
                if (exists != null)
                {
                    return Page();
                }
                    
            }
            
            var user = new Student {  Email = model.Email,  Name = model.Name};
            var hashedPassword = _passwordHasher.HashPassword(user, model.Password);
            user.Password = hashedPassword;
            _context.Students.Add(user);
            _context.SaveChanges();
            var token = _tokenGenerator.GenerateEmailVerificationToken(user, _secretKey.secretKey);
            var verificationLink = $"http://localhost:7100/Verify?token={token}";
            await _sender.SendEmailAsync(model.Email, "Email Verification", $"Please verify your email by clicking <a href='{verificationLink}'>here</a>.");

            return Page();


        }
    }
}
