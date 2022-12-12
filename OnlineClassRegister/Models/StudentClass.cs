using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClassRegister.Models
{
    public class StudentClass
    {

        [Key]
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Class tutor")]
        [ForeignKey("id")]
        public virtual Teacher? ClassTutor { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
        public virtual ICollection<Subject>? Subjects { get; set; }

        public StudentClass() 
        {
            Students = new HashSet<Student>();
            Subjects = new HashSet<Subject>();
        }
    }
}
