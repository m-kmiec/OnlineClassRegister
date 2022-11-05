namespace OnlineClassRegister.Models
{
    public class Teacher
    {
        private int id { get; set; }

        private string name { get; set; }

        private string surname { get; set; }

        private List<Subject> subjects { get; set; }
    }
}
