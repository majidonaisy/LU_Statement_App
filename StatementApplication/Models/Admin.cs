using System.ComponentModel.DataAnnotations;

namespace StatementApplication.Models
{
    public class Admin
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
    }
}
