using System.ComponentModel.DataAnnotations;

namespace OnlineClassRegister.Models
{
    public class Subject
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }

        public virtual ICollection<StudentClass>? classes { get; set; }
    }
}
