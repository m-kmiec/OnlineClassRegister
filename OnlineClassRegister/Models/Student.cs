using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClassRegister.Models
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Surrname")]
        public string surname { get; set; }
        public int studentClassId { get; set; }
        [DisplayName("Class")]
        public StudentClass? studentClass {get;set;}
    }
}
