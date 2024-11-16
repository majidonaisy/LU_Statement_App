using System.Diagnostics.Contracts;

namespace StatementApplication.Models
{
    public class Statement
    {
        public int Id { get; set; } 
        public string Type { get; set; }

        public string FirstNameEng { get; set; }
        public string MiddleNameEng { get; set; }
        public string LastNameEng { get; set; }
        
        public string FirstNameAra { get; set; }
        public string MiddleNameAra { get; set; }
        public string LastNameAra { get; set; }
        
        public DateTime DOB { get; set; }
        public string POB { get; set; }
        public long Phonenumber { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public DateTime SubmitionDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
        public string DoneBy { get; set; } = "";
        public string DeliveredBy { get; set; } = "";
        public int ApplicationId { get; set; }
        public string Comment { get; set; } = "";

    }
}
