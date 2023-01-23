using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineClassRegister.Areas.Identity.Data;

namespace OnlineClassRegister.Controllers
{
    public class TeacherMessagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineClassRegisterUser> _userManager;

        public TeacherMessagesController(ApplicationDbContext context, UserManager<OnlineClassRegisterUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var messages = _context.Message.Where(m => m.ReceiverUserId == currentUser.Id).ToList();
            var messageAndSender = messages.Select(message => new
                { message, sender = _context.Users.FirstOrDefault(u => u.Id == message.SenderUserId) });
            return View(messages);
        }

        public IActionResult Reply(int id)
        {
            var message = _context.Message.FirstOrDefault(m => m.Id == id);
            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> Reply(int id, string reply)
        {
            var message = _context.Message.FirstOrDefault(m => m.Id == id);
            message.Reply = reply;
            message.ReplyTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}