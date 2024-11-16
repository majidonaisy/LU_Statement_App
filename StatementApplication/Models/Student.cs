using System.ComponentModel.DataAnnotations;

namespace StatementApplication.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        
        public string Email { get; set; }

        public string Password { get; set; }
        public Boolean Verified { get; set; } = false;
        public ICollection<Statement> Statements { get; set; }

        public ICollection<Application> Applications { get; set;}



    }
}
