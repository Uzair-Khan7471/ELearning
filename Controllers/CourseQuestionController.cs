using LearningManagementSystem.Data;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Controllers
{
    public class CourseQuestionController : Controller
    {
        private readonly elearningContext db;

        public CourseQuestionController(elearningContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.CourseQuestions.Include(x=>x.CourseFkNavigation).ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.C = new SelectList(db.Courses, "Id" , "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseQuestion CQ_create)
        {
            if (ModelState.IsValid)
            {

                    db.Add(CQ_create);
                    await db.SaveChangesAsync();
                    TempData["message"] = "Data Inserted";
                    return RedirectToAction(nameof(Index));
                

            }
            ViewBag.C = new SelectList(db.Courses, "Id", "Title");

            return View(CQ_create);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.CourseQuestions.FindAsync(Id);
            if (dataa == null)
            {
                return NotFound();
            }
            ViewBag.C = new SelectList(db.Courses, "Id", "Title");

            return View(dataa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseQuestion CQ_edit)
        {
            if (CQ_edit.Id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    
                        db.Update(CQ_edit);
                        await db.SaveChangesAsync();
                    

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.CourseQuestions.Any(e => e.Id != CQ_edit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["message"] = "Data updated";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.C = new SelectList(db.Courses, "Id", "Title");
            return View(CQ_edit);
        }


        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.CourseQuestions.FirstOrDefaultAsync(x => x.Id == Id);
            if (dataa == null)
            {

                return NotFound();
            }
            db.CourseQuestions.Remove(dataa);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
