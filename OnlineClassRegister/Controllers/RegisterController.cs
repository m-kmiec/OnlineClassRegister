using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineClassRegister.Areas.Identity.Data;
using OnlineClassRegister.Models;

namespace OnlineClassRegister.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<OnlineClassRegisterUser> _userManager;

        public RegisterController(ApplicationDbContext context, UserManager<OnlineClassRegisterUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var userId = _userManager.GetUserName(HttpContext.User);
            var user = await _userManager.FindByEmailAsync(userId);
            Student loggedStudent = _context.Student
                .Where(s => s.name == user.FirstName && s.surname == user.LastName)
                .FirstOrDefault();
            ViewBag.subjectList = _context.Subject
                .Include(s => s.classes)
                .ThenInclude(c => c.students)
                .Where(s => s.classes.Any(c => c.students.Contains(loggedStudent)))
                .ToList();
            ViewBag.gradeList = _context.Grade
                .Where(g => g.studentId == loggedStudent.id)
                .ToList();
            ViewData["subjectSelectList"] = new SelectList(ViewBag.subjectList, "id", "name");
            return View();
        }

        [HttpPost]
        public IActionResult Index(string SelectedSubject)
        {
            return RedirectToAction("Index");
        }
    }
}
