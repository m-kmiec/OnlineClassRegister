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

        private int id { get; set; }

        private string name { get; set; }  

        private List<Student> students { get; set; }

        private Teacher classTutor { get; set; }

        private List<Subject>? subjects { get; set; }
    }
}
