using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineClassRegister.Areas.Identity.Data;
using OnlineClassRegister.Models;

namespace OnlineClassRegister.Controllers
{
    public class StudentClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentClasses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StudentClass.Include(s => s.classTutor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StudentClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentClass == null)
            {
                return NotFound();
            }

            var studentClass = await _context.StudentClass
                .Include(s => s.classTutor)
                .FirstOrDefaultAsync(m => m.id == id);
            if (studentClass == null)
            {
                return NotFound();
            }

            return View(studentClass);
        }

        // GET: StudentClasses/Create
        public IActionResult Create()
        {
            ViewData["id"] = new SelectList(_context.Teacher, "id", "id");
            return View();
        }

        // POST: StudentClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name")] StudentClass studentClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id"] = new SelectList(_context.Teacher, "id", "id", studentClass.id);
            return View(studentClass);
        }

        // GET: StudentClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentClass == null)
            {
                return NotFound();
            }

            var studentClass = await _context.StudentClass.FindAsync(id);
            if (studentClass == null)
            {
                return NotFound();
            }

            ViewBag.students = new MultiSelectList(_context.Student.ToList(), "id", "name", studentClass.students);

            return View(studentClass);
        }

        // POST: StudentClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,StudentIds")] StudentClass studentClass, string[] StudentIds)
        {
            if (id != studentClass.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var studentId in StudentIds)
                    {
                        Student student = new Student();
                        student.id = int.Parse(studentId);
                        student.studentClass = studentClass;
                    }
                    _context.Update(studentClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentClassExists(studentClass.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["id"] = new SelectList(_context.Teacher, "id", "id", studentClass.id);
            return View(studentClass);
        }

        // GET: StudentClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentClass == null)
            {
                return NotFound();
            }

            var studentClass = await _context.StudentClass
                .Include(s => s.classTutor)
                .FirstOrDefaultAsync(m => m.id == id);
            if (studentClass == null)
            {
                return NotFound();
            }

            return View(studentClass);
        }

        // POST: StudentClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentClass == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StudentClass'  is null.");
            }
            var studentClass = await _context.StudentClass.FindAsync(id);
            if (studentClass != null)
            {
                _context.StudentClass.Remove(studentClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentClassExists(int id)
        {
          return _context.StudentClass.Any(e => e.id == id);
        }
    }
}
