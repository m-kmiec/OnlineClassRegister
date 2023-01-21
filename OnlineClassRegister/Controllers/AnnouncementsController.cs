using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.DependencyResolver;
using OnlineClassRegister.Areas.Identity.Data;
using OnlineClassRegister.Models;

namespace OnlineClassRegister.Controllers;

public class AnnouncementsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<OnlineClassRegisterUser> _userManager;

    public AnnouncementsController(ApplicationDbContext context, UserManager<OnlineClassRegisterUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    [Authorize(Roles = "Parent, Teacher")]
    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var announcements = _context.Announcements.Where(a => a.Receivers.Any(r => r.ReceiverId == currentUser.Id))
            .ToList();
        return View(announcements);
    }
    [Authorize(Policy = "RequireTeacher")]
    public async Task<IActionResult> Create()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var teacher = _context.Teacher.FirstOrDefault(t => t.classTutoringId != null);

        if (teacher.name == currentUser.FirstName && teacher.surname == currentUser.LastName)
        {
            // throw some fancy error
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Announcement announcement)
    {
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

        return RedirectToAction("Index");
    }
}