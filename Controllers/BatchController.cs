using LearningManagementSystem.Data;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Controllers
{
    public class BatchController : Controller
    {
        private readonly elearningContext db;

        public BatchController(elearningContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Batches.Include(x=>x.UsersFkNavigation).Include(x => x.LabFkNavigation).ToListAsync());
        }


        public IActionResult Create()
        {
            ViewBag.user = new SelectList(db.Users, "Id", "Name");
            ViewBag.lab = new SelectList(db.Labs, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Batch B_create)
        {
            if (ModelState.IsValid)
            {

                if (db.Batches.Any(x => x.Name == B_create.Name))
                {
                    ViewBag.message = "Data Already Exsits";
                    ViewBag.user = new SelectList(db.Users, "Id", "Name");
                    ViewBag.lab = new SelectList(db.Labs, "Id", "Name");
                    return View(B_create);
                }
                else
                {
                    db.Add(B_create);
                    await db.SaveChangesAsync();
                    TempData["message"] = "Data Inserted";
                    return RedirectToAction(nameof(Index));
                }

            }
            ViewBag.user = new SelectList(db.Users, "Id", "Name");
            ViewBag.lab = new SelectList(db.Labs, "Id", "Name");
            return View(B_create);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Batches.FindAsync(Id);
            if (dataa == null)
            {
                return NotFound();
            }
            ViewBag.user = new SelectList(db.Users, "Id", "Name");
            ViewBag.lab = new SelectList(db.Labs, "Id", "Name");
            return View(dataa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Batch B_edit)
        {
            if (B_edit.Id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    
                        db.Update(B_edit);
                        await db.SaveChangesAsync();
                    

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Batches.Any(e => e.Id != B_edit.Id))
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
            ViewBag.user = new SelectList(db.Users, "Id", "Name");
            ViewBag.lab = new SelectList(db.Labs, "Id", "Name");
            return View(B_edit);
        }


        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Batches.FirstOrDefaultAsync(x => x.Id == Id);
            if (dataa == null)
            {

                return NotFound();
            }
            db.Batches.Remove(dataa);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
