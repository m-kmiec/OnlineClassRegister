namespace OnlineClassRegister.Models
{
    public class Subject
    {
        private int id { get; set; }

        private string name { get; set; }  

        private List<StudentClass> classes { get; set; }
    }
}
