using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClassRegister.Models
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }

        public string surname { get; set; }
        [ForeignKey("id")]

        public StudentClass studentClass;
    }
}
