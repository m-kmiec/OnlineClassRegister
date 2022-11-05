namespace OnlineClassRegister.Models
{
    public class StudentClass
    {
        public StudentClass(int id, string name, List<Student> students, Teacher classTutor, List<Subject>? subjects)
        {
            this.id = id;
            this.name = name;
            this.students = students;
            this.classTutor = classTutor;
            this.subjects = subjects;
        }

        public StudentClass() { }

        public int id { get; set; }

        public string name { get; set; }

        public virtual ICollection<Student> students { get; set; }

        public virtual Teacher classTutor { get; set; }

        public virtual ICollection<Subject>? subjects { get; set; }
    }
}
