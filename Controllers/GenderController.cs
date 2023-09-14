using LearningManagementSystem.Data;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Controllers
{
    public class GenderController : Controller
    {
        private readonly elearningContext db;

        public GenderController(elearningContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Genders.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gender G_create)
        {
            if (ModelState.IsValid)
            {

                if (db.Genders.Any(x => x.Gender1 == G_create.Gender1))
                {
                    ViewBag.message = "Data Already Exsits";
                    return View(G_create);
                }
                else
                {
                    db.Add(G_create);
                    await db.SaveChangesAsync();
                    TempData["message"] = "Data Inserted";
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(G_create);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Genders.FindAsync(Id);
            if (dataa == null)
            {
                return NotFound();
            }
            return View(dataa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Gender G_edit)
        {
            if (G_edit.Id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (db.Genders.Any(x => x.Gender1 == G_edit.Gender1))
                    {
                        ViewBag.message = "Data Already Exsits";
                        return View(G_edit);
                    }
                    else
                    {
                        db.Update(G_edit);
                        await db.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Genders.Any(e => e.Id != G_edit.Id))
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
            return View(G_edit);
        }


        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Genders.FirstOrDefaultAsync(x => x.Id == Id);
            if (dataa == null)
            {

                return NotFound();
            }
            db.Genders.Remove(dataa);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
