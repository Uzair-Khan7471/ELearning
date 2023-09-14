using LearningManagementSystem.Data;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly elearningContext db;

    public CategoryController(elearningContext context)
    {
        db = context;
    }
    
        public async Task<IActionResult> Index()
        {
            return View(await db.Categories.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category C_create)
        {
            if (ModelState.IsValid)
            {

                if (db.Categories.Any(x => x.Name == C_create.Name))
                {
                    ViewBag.message = "Data Already Exsits";
                    return View(C_create);
                }
                else
                {
                    db.Add(C_create);
                    await db.SaveChangesAsync();
                    TempData["message"] = "Data Inserted";
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(C_create);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Categories.FindAsync(Id);
            if (dataa == null)
            {
                return NotFound();
            }
            return View(dataa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category C_edit)
        {
            if (C_edit.Id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (db.Categories.Any(x => x.Name == C_edit.Name))
                    {
                        ViewBag.message = "Data Already Exsits";
                        return View(C_edit);
                    }
                    else
                    {
                        db.Update(C_edit);
                        await db.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Categories.Any(e => e.Id != C_edit.Id))
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
            return View(C_edit);
        }


        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (dataa == null)
            {

                return NotFound();
            }
            db.Categories.Remove(dataa);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
