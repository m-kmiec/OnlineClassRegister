using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineClassRegister.Models
{
    public class Grade
    {
        [Key] 
        public int Id { get; set; }
        [DisplayName("Grade")]
        public int value { get; set; }
        [DisplayName("Semester number")]
        public int semesterNumber { get; set; }
        public int teacherGradingId { get; set; }
        public virtual Teacher? TeacherGrading { get; set; }
        public int studentId { get; set; }

        public virtual Student? Student { get; set; }
        public int subjectId { get; set; }

        public virtual Subject? Subject { get; set; }
    }
}