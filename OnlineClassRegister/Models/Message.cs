using System.ComponentModel.DataAnnotations;

namespace OnlineClassRegister.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime MessageSendTime { get; set; }

        public string Text { get; set; }    
        [Required]

        public string SenderUserId { get; set; }
        [Required]

        public string ReceiverUserId { get; set; }

        public string? Reply { get; set; }

        public DateTime? ReplyTime { get; set; }
    }
}
