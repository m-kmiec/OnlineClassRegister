using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineClassRegister.Models
{
    public class StudentClass
    {

        [Key]
        public int id { get; set; }
        [DisplayName("Class Name")]
        public string name { get; set; }
        [DisplayName("Class tutor")]
        public virtual Teacher? classTutor { get; set; }

        public virtual ICollection<Student>? students { get; set; }
        public virtual ICollection<Subject>? subjects { get; set; }

        public StudentClass() 
        {
            students = new HashSet<Student>();
            subjects = new HashSet<Subject>();
        }
    }
}
