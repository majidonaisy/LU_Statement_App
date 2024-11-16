using System.ComponentModel.DataAnnotations;

namespace StatementApplication.Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }
        public List<Statement> Statements { get; set; } 
        public DateTime SubmissionDate { get; set; }
        public string Status { get; set; } = "Pending";
        public int StudentId { get; set; }
        public Student Student { get; set; }


    }
}
