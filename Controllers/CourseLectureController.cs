using LearningManagementSystem.Data;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Controllers
{
    public class CourseLectureController : Controller
    {
        private readonly elearningContext db;

        public CourseLectureController(elearningContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.CourseLectures.Include(x => x.CourseFkNavigation).ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.course = new SelectList(db.Courses, "Id", "Title");

            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseLecture CL_create, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                
                    if (file != null && file.Length > 0)
                    {
                        var extensioon = file.ContentType.ToLower();
                        Guid rand = Guid.NewGuid();
                        if (extensioon == "video/mp4")
                        {
                            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            var exten_pricese = extensioon.Substring(6);
                            var unique_name = fileName + rand + "." + exten_pricese;
                            string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Lectures");
                            if (!Directory.Exists(imagesFolder))
                            {
                                Directory.CreateDirectory(imagesFolder);
                            }
                            string filePath = Path.Combine(imagesFolder, unique_name);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            var dbAdress = Path.Combine("Lectures", unique_name);
                            CL_create.Video = dbAdress;
                            db.Add(CL_create);
                            await db.SaveChangesAsync();
                            TempData["message"] = "Data Inserted";
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ViewBag.message = "Video Format Error!";
                        ViewBag.course = new SelectList(db.Courses, "Id", "Title");

                        return View(CL_create);
                        }
                    }
                    else
                    {
                        ViewBag.message = "Select Some File";
                    ViewBag.course = new SelectList(db.Courses, "Id", "Title");

                    return View(CL_create);
                    }

            }
            ViewBag.course = new SelectList(db.Courses, "Id", "Title");

            return View(CL_create);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.CourseLectures.FindAsync(Id);
            if (dataa == null)
            {
                return NotFound();
            }
            TempData["video"] = dataa.Video;
            ViewBag.course = new SelectList(db.Courses, "Id", "Title");

            return View(dataa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseLecture CL_edit, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                if (ModelState.IsValid)
                {
                    var extensioon = file.ContentType.ToLower();
                    Guid rand = Guid.NewGuid();
                    if (extensioon == "video/mp4")
                    {
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var exten_pricese = extensioon.Substring(6);
                        var unique_name = fileName + rand + "." + exten_pricese;
                        string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Lectures");
                        if (!Directory.Exists(imagesFolder))
                        {
                            Directory.CreateDirectory(imagesFolder);
                        }
                        string filePath = Path.Combine(imagesFolder, unique_name);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        var dbAdress = Path.Combine("Lectures", unique_name);
                        CL_edit.Video = dbAdress;
                        db.Update(CL_edit);
                        await db.SaveChangesAsync();
                        TempData["message"] = "Data Updated";
                        return RedirectToAction(nameof(Index));
                    }

                    else
                    {
                        ViewBag.message = "Video Format Error!";
                        ViewBag.course = new SelectList(db.Courses, "Id", "Title");

                        return View(CL_edit);
                    }
                }
                ViewBag.course = new SelectList(db.Courses, "Id", "Title");

                return View(CL_edit);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    CL_edit.Video = TempData["video"].ToString();
                    db.Update(CL_edit);
                    db.SaveChangesAsync();
                    TempData["message"] = "Data Updated";
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.course = new SelectList(db.Courses, "Id", "Title");

                return View(CL_edit);
            }
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var dataa = await db.CourseLectures.FirstOrDefaultAsync(x => x.Id == Id);
            if (dataa == null)
            {

                return NotFound();
            }
            db.CourseLectures.Remove(dataa);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
