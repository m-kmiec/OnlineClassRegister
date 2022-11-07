namespace OnlineClassRegister.Models
{
    public class Teacher
    {
        public int id { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public virtual ICollection<Subject>? subjects { get; set; }
    }
}
