using System.ComponentModel.DataAnnotations;

namespace OnlineClassRegister.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        public virtual Teacher TeacherGrading { get; set; }

        public virtual Student Student { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
