using ITI_project.Models;
using ITI_project.ModelView;
using ITI_project.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITI_project.Controllers
{
    public class CourseController : Controller
    {
        IGenRepository<Course> crsRepo;
        IGenRepository<Department> deptRepo;
        public CourseController(IGenRepository<Course> _course, IGenRepository<Department> _department)
        {
            crsRepo = _course;
            deptRepo = _department;
        }

        public IActionResult Index()
        {
            List<Course> courses = crsRepo.GetAll();
            //Context context = new Context();
            //List<Course> courses = context.Courses.Include(x => x.Department).ToList();
            return View("Index", courses);
        }

        public IActionResult CheckMinDeg(int MinDegree, int Degree)
        {
            if (MinDegree > 50 | MinDegree < 20 | MinDegree == Degree)
                return Json(false);
            return Json(true);
        }
        public IActionResult SetHours(int Hours)
        {
            if (Hours > 100 | Hours < 1)
                return Json(false);
            return Json(true);
        }

        public IActionResult SetCookie()
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddYears(3);
            HttpContext.Response.Cookies.Append("Name", "Ahmed", options);
            return Content("Cookie saved");
        }
        public IActionResult GetCookie()
        {
            string name = HttpContext.Request.Cookies["Name"];
            return Content($"Name = {name}");
        }
        
        public IActionResult setSession()
        {
            HttpContext.Session.SetString("Name", "Ahmed");
            return Content("Data saved");
        }
        public IActionResult getSession()
        {
            string name = HttpContext.Session.GetString("Name");
            return Content($"Name = {name}");
        }

        public IActionResult New()
        {
            ViewBag.DeptList = deptRepo.GetAll();
            return View("New");
        }
        public IActionResult SaveNew(Course course)
        {
            if(ModelState.IsValid)
            {   if(course.DeptID != 0)
                {    
                crsRepo.Add(course);
                crsRepo.Save();
                    return RedirectToAction("Index");
                }
            }
                ViewBag.DeptList = deptRepo.GetAll();
                return View("New");

        }

        public IActionResult Delete(int id)
        {
            crsRepo.Remove(id);
            crsRepo.Save();
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            Course course = crsRepo.GetByID(id);
            ViewBag.DeptList = deptRepo.GetAll();
            return View("Edit", course);
        }
        public IActionResult SaveEdit(Course course)
        {
            if (ModelState.IsValid)
            {
                crsRepo.Update(course);
                crsRepo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.DeptList = deptRepo.GetAll();
            return View("Edit", course);
        }
    }
}
