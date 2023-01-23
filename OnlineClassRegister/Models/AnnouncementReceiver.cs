using Microsoft.AspNetCore.Identity;
using OnlineClassRegister.Areas.Identity.Data;

namespace OnlineClassRegister.Models
{
    public class AnnouncementReceiver
    {
        public int AnnouncementId { get; set; }
        public Announcement Announcement { get; set; }
        public string ReceiverId { get; set; }
        public OnlineClassRegisterUser Receiver { get; set; }
    }
}
