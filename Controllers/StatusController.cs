using LearningManagementSystem.Data;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Controllers
{
    public class StatusController : Controller
    {
        private readonly elearningContext db;

        public StatusController(elearningContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Statuses.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Status S_create)
        {
            if (ModelState.IsValid)
            {

                if (db.Statuses.Any(x => x.Status1 == S_create.Status1))
                {
                    ViewBag.message = "Data Already Exsits";
                    return View(S_create);
                }
                else
                {
                    db.Add(S_create);
                    await db.SaveChangesAsync();
                    TempData["message"] = "Data Inserted";
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(S_create);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Statuses.FindAsync(Id);
            if (dataa == null)
            {
                return NotFound();
            }
            return View(dataa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Status S_edit)
        {
            if (S_edit.Id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (db.Statuses.Any(x => x.Status1 == S_edit.Status1))
                    {
                        ViewBag.message = "Data Already Exsits";
                        return View(S_edit);
                    }
                    else
                    {
                        db.Update(S_edit);
                        await db.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Statuses.Any(e => e.Id != S_edit.Id))
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
            return View(S_edit);
        }


        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Statuses.FirstOrDefaultAsync(x => x.Id == Id);
            if (dataa == null)
            {

                return NotFound();
            }
            db.Statuses.Remove(dataa);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
