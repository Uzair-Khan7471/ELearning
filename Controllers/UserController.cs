using LearningManagementSystem.Data;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly elearningContext db;

        public UserController(elearningContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Users.Include(x => x.RoleFkNavigation).Include(x => x.GenderNavigation).Include(x => x.StatusFkNavigation).Include(x => x.BatchFkNavigation).ToListAsync());
        }


        public IActionResult Create()
        {
            ViewBag.role = new SelectList(db.Roles, "Id", "Name");
            ViewBag.gender = new SelectList(db.Genders, "Id", "Gender1");
            ViewBag.status = new SelectList(db.Statuses, "Id", "Name");
            ViewBag.batch = new SelectList(db.Batches, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User U_create, IFormFile file)
        {
            if (ModelState.IsValid)
            {

                if (db.Users.Any(x => x.Name == U_create.Name))
                {
                    ViewBag.message = "Data Already Exsits";
                    ViewBag.role = new SelectList(db.Roles, "Id", "Name");
                    ViewBag.gender = new SelectList(db.Genders, "Id", "Gender1");
                    ViewBag.status = new SelectList(db.Statuses, "Id", "Name");
                    ViewBag.batch = new SelectList(db.Batches, "Id", "Name");
                    return View(U_create);
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
                            string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Profile_Pic");
                            if (!Directory.Exists(imagesFolder))
                            {
                                Directory.CreateDirectory(imagesFolder);
                            }
                            string filePath = Path.Combine(imagesFolder, unique_name);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            var dbAdress = Path.Combine("Profile_Pic", unique_name);
                            U_create.Image = dbAdress;
                            db.Add(U_create);
                            await db.SaveChangesAsync();
                            TempData["message"] = "Data Inserted";
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ViewBag.message = "Image Format Error!";
                            ViewBag.role = new SelectList(db.Roles, "Id", "Name");
                            ViewBag.gender = new SelectList(db.Genders, "Id", "Gender1");
                            ViewBag.status = new SelectList(db.Statuses, "Id", "Name");
                            ViewBag.batch = new SelectList(db.Batches, "Id", "Name");
                            return View(U_create);
                        }
                    }
                    else
                    {
                        ViewBag.message = "Select Some File";
                        ViewBag.role = new SelectList(db.Roles, "Id", "Name");
                        ViewBag.gender = new SelectList(db.Genders, "Id", "Gender1");
                        ViewBag.status = new SelectList(db.Statuses, "Id", "Name");
                        ViewBag.batch = new SelectList(db.Batches, "Id", "Name");
                        return View(U_create);
                    }

                }

            }
            ViewBag.role = new SelectList(db.Roles, "Id", "Name");
            ViewBag.gender = new SelectList(db.Genders, "Id", "Gender1");
            ViewBag.status = new SelectList(db.Statuses, "Id", "Name");
            ViewBag.batch = new SelectList(db.Batches, "Id", "Name");
            return View(U_create);
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
            ViewBag.role = new SelectList(db.Roles, "Id", "Name");
            ViewBag.gender = new SelectList(db.Genders, "Id", "Gender1");
            ViewBag.status = new SelectList(db.Statuses, "Id", "Name");
            ViewBag.batch = new SelectList(db.Batches, "Id", "Name");
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
                        string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Profile_Pic");
                        if (!Directory.Exists(imagesFolder))
                        {
                            Directory.CreateDirectory(imagesFolder);
                        }
                        string filePath = Path.Combine(imagesFolder, unique_name);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        var dbAdress = Path.Combine("Profile_Pic", unique_name);
                        C_Edit.Image = dbAdress;
                        db.Update(C_Edit);
                        await db.SaveChangesAsync();
                        TempData["message"] = "Data Updated";
                        return RedirectToAction(nameof(Index));
                    }

                    else
                    {
                        ViewBag.message = "iMAGE FORMAT ERROR!";
                        ViewBag.role = new SelectList(db.Roles, "Id", "Name");
                        ViewBag.gender = new SelectList(db.Genders, "Id", "Gender1");
                        ViewBag.status = new SelectList(db.Statuses, "Id", "Name");
                        ViewBag.batch = new SelectList(db.Batches, "Id", "Name");
                        return View(C_Edit);
                    }
                }
                ViewBag.role = new SelectList(db.Roles, "Id", "Name");
                ViewBag.gender = new SelectList(db.Genders, "Id", "Gender1");
                ViewBag.status = new SelectList(db.Statuses, "Id", "Name");
                ViewBag.batch = new SelectList(db.Batches, "Id", "Name");
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
                ViewBag.role = new SelectList(db.Roles, "Id", "Name");
                ViewBag.gender = new SelectList(db.Genders, "Id", "Gender1");
                ViewBag.status = new SelectList(db.Statuses, "Id", "Name");
                ViewBag.batch = new SelectList(db.Batches, "Id", "Name");
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
