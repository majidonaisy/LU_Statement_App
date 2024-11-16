using System.ComponentModel.DataAnnotations;

namespace StatementApplication.Data.Enums
{
    public enum MasterType
    {
        [Display(Name = "افادة فصل اول انكليزي")]
        FirstSemesterEng,
        [Display(Name = "افادة فصل اول فرنسي")]
        FirstSemesterFr,
        [Display(Name = "افادة فصل ثاني انكليزي")]
        SecondSemesterEng,
        [Display(Name = "افادة فصل ثاني فرنسي")]
        SecondSemesterFr,
        [Display(Name = " انكليزي (افادة بمواد الماستر(للطلاب الغير حاصلين على جميع مواد الماستر")]
        CoursesStatementEng,
        [Display(Name = "فرنسي (افادة بمواد الماستر(للطلاب الغير حاصلين على جميع مواد الماستر")]
        CoursesStatementFr,
        [Display(Name = "افادة انهاء ماستر بالعلامات انكليزي")]
        MasterFinishEng,
        [Display(Name = "افادة انهاء ماستر بالعلامات فرنسي")]
        MasterFinishFr,
        [Display(Name = "افادة انهاء ماستر بدون علامات انكليزي")]
        MasterFinishGradesEng,
        [Display(Name = "افادة انهاء ماستر بدون علامات فرنسي")]
        MasterFinishGradesFr,
        [Display(Name = "افادة انهاء ماستر بدون علامات باللغة العربية(اجازة تعليمية)")]
        MasterFinishAr
    }
}
