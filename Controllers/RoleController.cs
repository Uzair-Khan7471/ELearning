using LearningManagementSystem.Data;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Controllers
{
    public class RoleController : Controller
    {
        private readonly elearningContext db;

        public RoleController(elearningContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Roles.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role R_create)
        {
            if (ModelState.IsValid)
            {

                if (db.Roles.Any(x => x.Name == R_create.Name))
                {
                    ViewBag.message = "Data Already Exsits";
                    return View(R_create);
                }
                else
                {
                    db.Add(R_create);
                    await db.SaveChangesAsync();
                    TempData["message"] = "Data Inserted";
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(R_create);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Roles.FindAsync(Id);
            if (dataa == null)
            {
                return NotFound();
            }
            return View(dataa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Role R_edit)
        {
            if (R_edit.Id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (db.Roles.Any(x => x.Name == R_edit.Name))
                    {
                        ViewBag.message = "Data Already Exsits";
                        return View(R_edit);
                    }
                    else
                    {
                        db.Update(R_edit);
                        await db.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Roles.Any(e => e.Id != R_edit.Id))
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
            return View(R_edit);
        }


        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Roles.FirstOrDefaultAsync(x => x.Id == Id);
            if (dataa == null)
            {

                return NotFound();
            }
            db.Roles.Remove(dataa);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
