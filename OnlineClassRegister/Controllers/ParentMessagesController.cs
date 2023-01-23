using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineClassRegister.Areas.Identity.Data;
using System.Security.Claims;
using OnlineClassRegister.Models;
using Microsoft.AspNetCore.Identity;

namespace OnlineClassRegister.Controllers;

public class ParentMessagesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<OnlineClassRegisterUser> _userManager;

    public ParentMessagesController(ApplicationDbContext context, UserManager<OnlineClassRegisterUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var messages = _context.Message.Where(m => m.SenderUserId == currentUser.Id).ToList();
        return View(messages);
    }
    public IActionResult Create()
    {
        var users = _userManager.GetUsersInRoleAsync("Teacher").Result;
        ViewBag.Users = new SelectList(users, "Id", "UserName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string receiverUserId, string text)
    {
        var senderUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var messageToSend = new Message
        {
            MessageSendTime = DateTime.Now,
            SenderUserId = senderUserId,
            ReceiverUserId = receiverUserId,
            Text = text
        };

        _context.Message.Add(messageToSend);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "ParentMessages");
    }
}