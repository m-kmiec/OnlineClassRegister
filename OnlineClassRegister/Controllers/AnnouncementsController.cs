using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using NuGet.DependencyResolver;
using OnlineClassRegister.Areas.Identity.Data;
using OnlineClassRegister.Models;
using OnlineClassRegister.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OnlineClassRegister.Controllers;

public class AnnouncementsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<OnlineClassRegisterUser> _userManager;
    public AuthMessageSenderOptions Options { get; }

    public AnnouncementsController(ApplicationDbContext context, UserManager<OnlineClassRegisterUser> userManager,
        IOptions<AuthMessageSenderOptions> optionsAccessor)
    {
        _context = context;
        _userManager = userManager;
        Options = optionsAccessor.Value;
    }

    [Authorize(Roles = "Parent, Teacher")]
    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var announcements = _context.Announcements.Where(a => a.Receivers.Any(r => r.ReceiverId == currentUser.Id))
            .ToList();
        return View(announcements);
    }

    public async Task<IActionResult> AnnouncementsList()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var announcements = _context.Announcements.Where(a => a.SenderId == currentUser.Id).ToList();

        return View(announcements);
    }

    [Authorize(Policy = "RequireTeacher")]
    public async Task<IActionResult> Create()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var teacher = _context.Teacher.FirstOrDefault(t => t.classTutoringId != null);

        if (teacher.name == currentUser.FirstName && teacher.surname == currentUser.LastName)
        {
            return NotFound();
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Announcement announcement)
    {
        announcement.SenderId = _userManager.GetUserId(User);
        announcement.CreatedAt = DateTime.Now;
        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();

        var teacher = _context.Teacher.FirstOrDefault(t => t.classTutoringId != null);
        var parents = await _userManager.GetUsersInRoleAsync("Parent");
        var parentsForStudentGroup = parents.Where(p => p.StudentGroupId == teacher.classTutoringId).ToList();

        var receivers = parentsForStudentGroup.Select(u => new AnnouncementReceiver
            { AnnouncementId = announcement.Id, ReceiverId = u.Id });

        _context.AnnouncementReceivers.AddRange(receivers);
        await _context.SaveChangesAsync();

        return RedirectToAction("AnnouncementsList");
    }

    [HttpPost]
    public async Task<IActionResult> SendAsEmail(Announcement announcement)
    {
        // create announcement and save it to db

        announcement.SenderId = _userManager.GetUserId(User);
        announcement.CreatedAt = DateTime.Now;
        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();

        var teacher = _context.Teacher.FirstOrDefault(t => t.classTutoringId != null);
        var parents = await _userManager.GetUsersInRoleAsync("Parent");
        var parentsForStudentGroup = parents.Where(p => p.StudentGroupId == teacher.classTutoringId).ToList();

        var receivers = parentsForStudentGroup.Select(u => new AnnouncementReceiver
            { AnnouncementId = announcement.Id, ReceiverId = u.Id });

        _context.AnnouncementReceivers.AddRange(receivers);
        await _context.SaveChangesAsync();

        // send emails

        var client = new SendGridClient(Options.SendGridKey);

        List<EmailAddress> recipients = new List<EmailAddress>();

        foreach (var parent in parents)
        {
            recipients.Add(new EmailAddress(parent.Email));
        }

        var msg = new SendGridMessage();
        msg.SetFrom(new EmailAddress("onlineclassregister1@gmail.com"));
        msg.AddTos(recipients);
        msg.SetSubject(announcement.Title);
        msg.AddContent(MimeType.Text, announcement.Description);

        await client.SendEmailAsync(msg);

        return RedirectToAction("AnnouncementsList");
    }
}