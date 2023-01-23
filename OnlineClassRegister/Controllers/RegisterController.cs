using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineClassRegister.Areas.Identity.Data;
using OnlineClassRegister.Models;

namespace OnlineClassRegister.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<OnlineClassRegisterUser> _userManager;

        private Dictionary<Student, List<Grade>> studentWithGrades { get; set; }

        private static String[] classSubject { get; set; }

        private static Teacher loggedTeacher { get; set; }

        private static List<Student> Students { get; set; }

        public RegisterController(ApplicationDbContext context, UserManager<OnlineClassRegisterUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            studentWithGrades = new Dictionary<Student, List<Grade>>();
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

        [HttpGet]
        public async Task<IActionResult> IndexTeacherAsync()
        {
            var userId = _userManager.GetUserName(HttpContext.User);
            var user = await _userManager.FindByEmailAsync(userId);
            loggedTeacher = _context.Teacher
                .Where(s => s.name == user.FirstName && s.surname == user.LastName)
                .FirstOrDefault();
            Dictionary<Subject, List<StudentClass>> dictionary = new Dictionary<Subject, List<StudentClass>>();
            List<Subject> subjects = _context.Subject
                .Where(s => s.teachers.Contains(loggedTeacher))
                .ToList();
            foreach (Subject s in subjects)
            {
                dictionary.Add(s, _context.StudentClass
                    .Where(sc => sc.subjects.Contains(s)).ToList());
            }

            ViewBag.data = dictionary;
            return View();
        }
        [HttpPost]
        public IActionResult IndexTeacher(string SelectedOption)
        {
            classSubject = SelectedOption.Split(",");
            Students = _context.Student
                .Where(s => s.studentClass.name == classSubject[0])
                .ToList();
            if (studentWithGrades.Any())
                studentWithGrades.Clear();
            foreach (Student student in Students)
            {
                studentWithGrades.Add(student, _context.Grade
                    .Where(g => g.Student == student && g.Subject.name == classSubject[1])
                    .ToList());
            }

            string table = UpdateTable();

            ViewBag.Table = table;

            return Content(table, "text/html");
        }

        private string UpdateTable()
        {
            var htmlRaw = new StringBuilder();
            double numberOfGrades,mean,numberOfAllGrades=0,totalMean=0;
            htmlRaw.Append("<table id=\"tableHidden\" class=\"tg\">\r\n    <thead>\r\n        <tr>\r\n            <th class=\"tg-0pky\" rowspan=\"2\">Student</th>\r\n            <th class=\"tg-0pky\" colspan=\"2\">Semester 1</th>\r\n            <th class=\"tg-0pky\" colspan=\"2\">Semester 2</th>\r\n            <th class=\"tg-c3ow\" rowspan=\"2\">Avg. Year</th>\r\n        </tr>\r\n        <tr>\r\n            <th class=\"tg-0pky\">Grades</th>\r\n            <th class=\"tg-0pky\">Avg. I</th>\r\n            <th class=\"tg-0pky\">Grades</th>\r\n            <th class=\"tg-0pky\">Avg. II</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>");
            foreach (var item in studentWithGrades)
            {
                htmlRaw.Append("<tr>");
                htmlRaw.Append("<td class=\"tg-0pky\">" + item.Key.name + " " + item.Key.surname + "</td>");
                numberOfGrades = 0;
                mean = 0;
                htmlRaw.Append("<td class=\"tg-0pky\">");
                foreach(var grade in item.Value)
                {
                    if (grade.semesterNumber == 1)
                    {
                        totalMean += grade.value;
                        numberOfAllGrades++;
                        numberOfGrades++;
                        mean += grade.value;
                        htmlRaw.Append("<div class=\"gradeBox\">" + grade.value + "</div>");
                    }
                }
                htmlRaw.Append("</td>");
                
                htmlRaw.Append("<td class=\"tg-0pky\">");
                htmlRaw.Append(string.Format("{0:0.00}", mean == 0.0 ? 0 : mean / numberOfGrades));
                htmlRaw.Append("</td>");

                numberOfGrades = 0;
                mean = 0;
                htmlRaw.Append("<td class=\"tg-0pky\">");
                foreach (var grade in item.Value)
                {
                    if (grade.semesterNumber == 2)
                    {
                        totalMean += grade.value;
                        numberOfAllGrades++;
                        numberOfGrades++;
                        mean += grade.value;
                        htmlRaw.Append("<div class=\"gradeBox\">" + grade.value + "</div>");
                    }
                }
                htmlRaw.Append("</td>");


                htmlRaw.Append("<td class=\"tg-0pky\">");
                htmlRaw.Append(string.Format("{0:0.00}", mean == 0.0 ? 0 : mean / numberOfGrades));
                htmlRaw.Append("</td>");

                htmlRaw.Append("<td class=\"tg-0pky\">");
                htmlRaw.Append(string.Format("{0:0.00}", totalMean == 0.0 ? 0 : totalMean / numberOfAllGrades));
                htmlRaw.Append("</td>");
                htmlRaw.Append("</tr>");
            }
            htmlRaw.Append("</tbody>");
            htmlRaw.Append("</table>");
            return htmlRaw.ToString();
        }

        
        [HttpPost]
        public IActionResult ShowForm()
        {
            ViewBag.students = new SelectList(Students, "id","surname");
            return PartialView("AddGrade");
        }

        [HttpGet]
        public IActionResult AddGrade()
        {
            return View("AddGrade");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGradeAsync(Grade grade)
        {
            if (ModelState.IsValid)
            {
                grade.TeacherGrading = _context.Teacher
                    .Where(t => t.id == loggedTeacher.id)
                    .First();
                grade.Subject = _context.Subject
                    .Where(s => s.name == classSubject[1])
                    .First();
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexTeacher");
            }

            return View(grade);
        }
    }
}
