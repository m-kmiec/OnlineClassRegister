using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClassRegister.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        public virtual ICollection<StudentClass> Classes { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }

        public Subject()
        {
            this.Classes = new HashSet<StudentClass>();
            this.Teachers = new HashSet<Teacher>();
        }
    }
}
