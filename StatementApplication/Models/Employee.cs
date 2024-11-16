using System.ComponentModel.DataAnnotations;

namespace StatementApplication.Models
{
    public class Employee
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
    }
}
