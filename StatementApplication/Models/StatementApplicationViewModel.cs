using System.ComponentModel.DataAnnotations;

namespace StatementApplication.Models
{
    public class StatementApplicationViewModel
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string FirstNameEng { get; set; }
        [Required]
        public string MiddleNameEng { get; set; }
        [Required]
        public string LastNameEng { get; set; }
        [Required]
        public string FirstNameAra { get; set; }
        [Required]
        public string MiddleNameAra { get; set; }

        [Required]
        public string LastNameAra { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string POB { get; set; }

        [Required]
        public long Phonenumber { get; set; }

        [Required]
        public int StudentId { get; set; }
    }
}
