using LearningManagementSystem.Data;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly elearningContext db;

        public CourseController(elearningContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Courses.Include(x => x.CategoryFkNavigation).Include(x => x.UsersFkNavigation).ToListAsync());
        }


        public IActionResult Create()
        {
            ViewBag.category = new SelectList(db.Categories, "Id" , "Name");
            ViewBag.user = new SelectList(db.Users, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course C_create, IFormFile file)
        {
            if (ModelState.IsValid)
            {

                if (db.Courses.Any(x => x.Title == C_create.Title))
                {
                    ViewBag.message = "Data Already Exsits";
                    ViewBag.category = new SelectList(db.Categories, "Id", "Name");
                    ViewBag.user = new SelectList(db.Users, "Id", "Name"); return View(C_create);
                }
                else
                {
                    if (file != null && file.Length > 0)
                    {
                        var extensioon = file.ContentType.ToLower();
                        Guid rand = Guid.NewGuid();
                        if (extensioon == "image/jpg" || extensioon == "image/png" || extensioon == "image/jpeg")
                        {
                            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            var exten_pricese = extensioon.Substring(6);
                            var unique_name = fileName + rand + "." + exten_pricese;
                            string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Course");
                            if (!Directory.Exists(imagesFolder))
                            {
                                Directory.CreateDirectory(imagesFolder);
                            }
                            string filePath = Path.Combine(imagesFolder, unique_name);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            var dbAdress = Path.Combine("Course", unique_name);
                            C_create.Image = dbAdress;
                            db.Add(C_create);
                            await db.SaveChangesAsync();
                            TempData["message"] = "Data Inserted";
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ViewBag.message = "Image Format Error!";
                            ViewBag.category = new SelectList(db.Categories, "Id", "Name");
                            ViewBag.user = new SelectList(db.Users, "Id", "Name");
                            return View(C_create);
                        }
                    }
                    else
                    {
                        ViewBag.message = "Select Some File";
                        ViewBag.category = new SelectList(db.Categories, "Id", "Name");
                        ViewBag.user = new SelectList(db.Users, "Id", "Name");
                        return View(C_create);
                    }

                }

            }
            ViewBag.category = new SelectList(db.Categories, "Id", "Name");
            ViewBag.user = new SelectList(db.Users, "Id", "Name");
            return View(C_create);
        }


        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Courses.FindAsync(Id);
            if (dataa == null)
            {
                return NotFound();
            }
            TempData["image"] = dataa.Image;
            ViewBag.category = new SelectList(db.Categories, "Id", "Name");
            ViewBag.user = new SelectList(db.Users, "Id", "Name");
            return View(dataa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course C_Edit, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                if (ModelState.IsValid)
                {
                    var extensioon = file.ContentType.ToLower();
                    Guid rand = Guid.NewGuid();
                    if (extensioon == "image/jpg" || extensioon == "image/png" || extensioon == "image/jpeg")
                    {
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var exten_pricese = extensioon.Substring(6);
                        var unique_name = fileName + rand + "." + exten_pricese;
                        string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Course");
                        if (!Directory.Exists(imagesFolder))
                        {
                            Directory.CreateDirectory(imagesFolder);
                        }
                        string filePath = Path.Combine(imagesFolder, unique_name);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        var dbAdress = Path.Combine("Course", unique_name);
                        C_Edit.Image = dbAdress;
                        db.Update(C_Edit);
                        await db.SaveChangesAsync();
                        TempData["message"] = "Data Updated";
                        return RedirectToAction(nameof(Index));
                    }

                    else
                    {
                        ViewBag.message = "iMAGE FORMAT ERROR!";
                        ViewBag.category = new SelectList(db.Categories, "Id", "Name");
                        ViewBag.user = new SelectList(db.Users, "Id", "Name");
                        return View(C_Edit);
                    }
                }
                ViewBag.category = new SelectList(db.Categories, "Id", "Name");
                ViewBag.user = new SelectList(db.Users, "Id", "Name");
                return View(C_Edit);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    C_Edit.Image = TempData["image"].ToString();
                    db.Update(C_Edit);
                    db.SaveChangesAsync();
                    TempData["message"] = "Data Updated";
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.category = new SelectList(db.Categories, "Id", "Name");
                ViewBag.user = new SelectList(db.Users, "Id", "Name");
                return View(C_Edit);
            }
        }


        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.Courses.FirstOrDefaultAsync(x => x.Id == Id);
            if (dataa == null)
            {

                return NotFound();
            }
            db.Courses.Remove(dataa);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
