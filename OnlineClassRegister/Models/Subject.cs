using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClassRegister.Models
{
    public class Subject
    {
        [Key]
        public int id { get; set; }
        [DisplayName("Name")]
        public string name { get; set; }
        public virtual ICollection<StudentClass>? classes { get; set; }
    }
}
