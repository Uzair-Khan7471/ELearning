using LearningManagementSystem.Data;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Controllers
{
    public class LabController : Controller
    {
        private readonly elearningContext db;

        public LabController(elearningContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Labs.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lab L_create)
        {
            if (ModelState.IsValid)
            {

                if (db.Labs.Any(x => x.Name == L_create.Name))
                {
                    ViewBag.message = "Data Already Exsits";
                    return View(L_create);
                }
                else
                {
                    db.Add(L_create);
                    await db.SaveChangesAsync();
                    TempData["message"] = "Data Inserted";
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(L_create);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Labs.FindAsync(Id);
            if (dataa == null)
            {
                return NotFound();
            }
            return View(dataa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Lab L_edit)
        {
            if (L_edit.Id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {

                    db.Update(L_edit);
                    await db.SaveChangesAsync();


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Labs.Any(e => e.Id != L_edit.Id))
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
            return View(L_edit);
        }


        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Labs.FirstOrDefaultAsync(x => x.Id == Id);
            if (dataa == null)
            {

                return NotFound();
            }
            db.Labs.Remove(dataa);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
