using LearningManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly elearningContext db;

        public DashboardController(elearningContext context) => db = context;
        public IActionResult Index()
        {
            ViewBag.course = db.Courses.Count();
            ViewBag.batch = db.Batches.Count();
            ViewBag.lab = db.Labs.Count();
            ViewBag.category = db.Categories.Count();

            return View();
        }
    }
}
