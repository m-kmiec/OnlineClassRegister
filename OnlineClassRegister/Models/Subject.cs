namespace OnlineClassRegister.Models
{
    public class Subject
    {
        public int id { get; set; }

        public string name { get; set; }

        public virtual ICollection<StudentClass> classes { get; set; }
    }
}
