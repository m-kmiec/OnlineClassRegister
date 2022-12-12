using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineClassRegister.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Surrname")]
        public string Surname { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }

        public Teacher()
        {
            Subjects = new HashSet<Subject>();
        }

    }
}
