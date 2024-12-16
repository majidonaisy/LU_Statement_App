using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace StatementApplication.Controllers
{
    [Route("/api/logout")]
    public class LogoutController : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); // Removes the authentication cookie
            return Ok(new { message = "Logged out successfully" }); // Respond with success
        }
    }
}
