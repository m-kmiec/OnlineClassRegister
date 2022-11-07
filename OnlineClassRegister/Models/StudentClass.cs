namespace OnlineClassRegister.Models
{
    public class StudentClass
    {

        public StudentClass() { }

        public int id { get; set; }

        public string name { get; set; }

        public virtual ICollection<Student>? students { get; set; }

        public virtual Teacher classTutor { get; set; }

        public virtual ICollection<Subject>? subjects { get; set; }
    }
}
