using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITI_project.Models;
using ITI_project.ModelView;
using Microsoft.Identity.Client;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ITI_project.Controllers
{
    public class InstructorController : Controller
    {
        Context context = new Context();
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ShowAll()
        {
            List<Instructor> instructors = context.Instructores.ToList();
            return View("ShowAll", instructors);
        }           
        public IActionResult Details(int id)
        {
            Instructor instructor = context.Instructores.Include(x => x.Department).FirstOrDefault(x => x.Id == id);
            instructor = context.Instructores.Include(x => x.Course).FirstOrDefault(x => x.Id == id);
            return View("Details", instructor);
        }

        [HttpGet]
        public IActionResult New()
        {
            ListOfCoursesAndDepartments listes = new ListOfCoursesAndDepartments();
            listes.Courses = context.Courses.ToList();
            listes.Departments = context.Departments.ToList();
            ViewBag.listes = listes;
            return View("New");
        }
        public IActionResult DeptToCourses(int DeptId)
        {
            return Json(context.Courses.Where(x => x.DeptID == DeptId).ToList());
        }
        //save new
        [HttpPost]
        public IActionResult Add(Instructor instructor)
        {
            if (instructor.Name == null | instructor.Image == null | instructor.DeptID == 0)
            {
                ListOfCoursesAndDepartments listes = new ListOfCoursesAndDepartments();
                listes.Courses = context.Courses.ToList();
                listes.Departments = context.Departments.ToList();
                ViewBag.listes = listes;
                return View("New");
            }

            string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string fileName = Path.GetFileNameWithoutExtension(instructor.Image.FileName);
            string extension = Path.GetExtension(instructor.Image.FileName);
            string filePath = Path.Combine(wwwRootPath, "images", fileName + extension);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    instructor.Image.CopyToAsync(fileStream);
                }

                instructor.Img = "/images/" + Path.GetFileName(filePath);

                context.Instructores.Add(instructor);

            context.SaveChanges();
            return RedirectToAction("ShowAll");
        }

        public IActionResult Delete(int id)
        {
            Instructor instructor = context.Instructores.FirstOrDefault(x => x.Id == id);
            context.Instructores.Remove(instructor);
            context.SaveChanges();
            return RedirectToAction("ShowAll");
        }   

        public IActionResult Edit(int id)
        {
            Instructor instructor = context.Instructores.FirstOrDefault(x => x.Id == id);

            InstructorWithLists cap = new InstructorWithLists();
            cap.Id = instructor.Id;
            cap.Name = instructor.Name;
            cap.Address = instructor.Address;
            cap.DeptID = instructor.DeptID;
            cap.CourseID = instructor.CourseID;
            cap.Img = instructor.Img;
            cap.Salary = instructor.Salary;
            cap.lists.Courses = context.Courses.ToList();
            cap.lists.Departments = context.Departments.ToList();

            return View("Edit", cap);
        }
        [HttpPost]
        public IActionResult SaveEdit(InstructorWithLists obj)
        {
            if(obj.Name == null | obj.Salary == null | obj.Address == null )
            {
                obj.lists.Courses = context.Courses.ToList();
                obj.lists.Departments = context.Departments.ToList();
                return View("Edit", obj);
            }
            Instructor instructor = context.Instructores.FirstOrDefault(x => x.Id == obj.Id);
            instructor.Name = obj.Name;
            instructor.Address = obj.Address;
            instructor.DeptID = obj.DeptID;
            instructor.CourseID = obj.CourseID;
            instructor.Img = obj.Img;
            instructor.Salary = obj.Salary;

            context.SaveChanges();
            return RedirectToAction("ShowAll");

        }
    }
}
