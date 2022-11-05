namespace OnlineClassRegister.Models
{
    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }

        public string surname { get; set; }

        public virtual StudentClass studentClass;
    }
}
