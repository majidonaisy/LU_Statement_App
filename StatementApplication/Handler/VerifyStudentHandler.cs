using Microsoft.AspNetCore.Authorization;
using StatementApplication.AuthorizationRequirements;
using StatementApplication.Data;
using System.Security.Claims;

namespace StatementApplication.Handler
{
    public class VerifyStudentHandler : AuthorizationHandler<VerifiedStudentRequirement>
    {
        private readonly AppDataContext _context;
        public VerifyStudentHandler(AppDataContext appDataContext)
        {
            _context = appDataContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, VerifiedStudentRequirement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(ClaimTypes.Sid)?.Value);

            if (userId == null)
            {
                context.Fail(); // If user is not authenticated, fail
                return Task.CompletedTask;
            }

            // Fetch user from the database
            var student = _context.Students.FirstOrDefault(s => s.StudentId == userId);

            if (student != null && student.Verified)
            {
                context.Succeed(requirement); // User is verified
            }
            else
            {
                context.Fail(); // User is not verified
            }

            return Task.CompletedTask;
        }
    }
}
