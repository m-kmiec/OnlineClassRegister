namespace OnlineClassRegister.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<AnnouncementReceiver> Receivers { get; set; }
    }
}
