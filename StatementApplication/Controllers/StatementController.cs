using Microsoft.AspNetCore.Mvc;
using StatementApplication.Data;
using System.Text.Json;

namespace StatementApplication.Controllers
{
    [Route("/api/statement")]
    public class StatementController : ControllerBase
    {
        public AppDataContext _context;
        public StatementController(AppDataContext context)
        {
            _context = context;
        }
        [HttpPost("accept")]
        public IActionResult MarkAsAccepted([FromBody] JsonElement requestData)
        {
            if(requestData.TryGetProperty("statementId", out JsonElement statementIdElement))
            {
                int statementId = statementIdElement.GetInt32();
                var statement = _context.Statement.FirstOrDefault(x => x.Id == statementId);
                if (statement != null)
                {
                    statement.Status = "Accepted"; // or any other logic
                    _context.SaveChanges();
                }

                return new JsonResult(new { success = true });
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }
        [HttpPost("deny")]
        public IActionResult MarkAsDenied([FromBody] JsonElement requestData)
        {
            if (requestData.TryGetProperty("statementId", out JsonElement statementIdElement))
            {
                int statementId = statementIdElement.GetInt32();
                var statement = _context.Statement.FirstOrDefault(x => x.Id == statementId);
                if (statement != null)
                {
                    statement.Status = "Denied"; // or any other logic
                    _context.SaveChanges();
                }

                return new JsonResult(new { success = true });
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }
    }
}
