namespace StatementApplication.Models
{
    public class StatementApplicationModel
    {
        public List<string> Type { get; set; }
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
    }
}
