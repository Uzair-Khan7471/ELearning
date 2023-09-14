using LearningManagementSystem.Data;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Controllers
{
    public class CourseAnswerController : Controller
    {
        private readonly elearningContext db;

        public CourseAnswerController(elearningContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.CourseAnswers.Include(x => x.CourseQuestionFkNavigation).ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.CQ = new SelectList(db.CourseQuestions, "Id" , "Questions");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseAnswer CA_create)
        {
            if (ModelState.IsValid)
            {

                    db.Add(CA_create);
                    await db.SaveChangesAsync();
                    TempData["message"] = "Data Inserted";
                    return RedirectToAction(nameof(Index));
                

            }
            ViewBag.CQ = new SelectList(db.CourseQuestions, "Id", "Questions");

            return View(CA_create);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.CourseAnswers.FindAsync(Id);
            if (dataa == null)
            {
                return NotFound();
            }
            ViewBag.CQ = new SelectList(db.CourseQuestions, "Id", "Questions");

            return View(dataa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseAnswer CA_edit)
        {
            if (CA_edit.Id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    
                        db.Update(CA_edit);
                        await db.SaveChangesAsync();
                    

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.CourseAnswers.Any(e => e.Id != CA_edit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["message"] = "Data updaed";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CQ = new SelectList(db.CourseQuestions, "Id", "Questions");

            return View(CA_edit);
        }


        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.CourseAnswers.FirstOrDefaultAsync(x => x.Id == Id);
            if (dataa == null)
            {

                return NotFound();
            }
            db.CourseAnswers.Remove(dataa);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
