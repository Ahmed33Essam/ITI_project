using ITI_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace ITI_project.Controllers
{
    public class CourseResController : Controller
    {
        Context context = new Context();

        [HttpGet]
        public IActionResult ChooseResType()
        {
            int id = 0;
            return View("ChooseResType", id);
        }
        [HttpPost]
        public IActionResult Go(int id)
        {
            if (id == 1)
                return RedirectToAction("ShowRes");
            else if (id == 2)
                return RedirectToAction("CoursesRes");
            else if (id == 3)
                return RedirectToAction("TraineeRes");
            return View("ChooseResType");
        }

        [HttpGet]
        public IActionResult ShowRes()  
        {
            ViewBag.Courses = context.Courses.ToList();
            return View("ShowRes");
        }
        [HttpPost]
        public IActionResult ShowResOutput(CourseRes obj)
        {
            if(ModelState.IsValid)
            {
                if(obj.CourseID != 0)
                {
                    CourseRes res = context.CourseRes.FirstOrDefault(x => x.TraineeID == obj.TraineeID && x.CourseID == obj.CourseID);
                    if(res != null)
                    {
                        ViewBag.trainee = context.Trainees.FirstOrDefault(x => x.Id == res.TraineeID);
                        Course course = context.Courses.FirstOrDefault(x => x.Id == res.CourseID);
                        ViewBag.course = course;

                        if (course.MinDegree > res.Degree)
                            { ViewBag.color = "red"; }
                        else
                            { ViewBag.color = "green"; }

                            return View("ShowResOutput", res);
                    }
                }
            }
            ViewBag.Courses = context.Courses.ToList();
            return View("ShowRes", obj);
        }

        public IActionResult ISTraineeIDCorrect(int TraineeID)
        {
            Trainee? trainee = context.Trainees.FirstOrDefault(x => x.Id == TraineeID);
            if (trainee == null)
                return Json(false);
            return Json(true);
        }

        [HttpGet]
        public IActionResult CoursesRes()
        {
            ViewBag.courses = context.Courses.ToList();
            return View("CoursesRes");
        }
        [HttpPost]
        public IActionResult ShowCourses(Course obj)
        {
            if(obj.Id == 0)
            {
                ViewBag.courses = context.Courses.ToList();
                return View("CoursesRes");
            }

            Course? course = context.Courses.FirstOrDefault(x => x.Id == obj.Id);
            List<CourseRes> res = context.CourseRes.Where(x => x.CourseID == obj.Id).ToList();
            List<(string, int, string)> pairs = new List<(string, int, string)>();

            int minD = course.MinDegree;
            string color;
            foreach(var item in res)
            {
                Trainee trainee = context.Trainees.FirstOrDefault(x => x.Id == item.TraineeID);
                if (item.Degree < minD)
                    color = "Red";
                else
                    color = "Green";

                    pairs.Add((trainee.Name, item.Degree, color));
            }
            return View("ShowCourses", pairs);
        }

        [HttpGet]
        public IActionResult TraineeRes()
        {
            return View("TraineeRes");
        }
        [HttpPost]
        public IActionResult TraineeShow(Trainee obj)
        {
            Trainee? trainee = context.Trainees.FirstOrDefault(x => x.Id == obj.Id);
            if (trainee == null)
                { ViewBag.msg = "ID not found";
                return View("TraineeRes"); }
            ViewBag.trainee = trainee.Name;
            List<CourseRes> res = context.CourseRes.Where(x => x.TraineeID == trainee.Id).ToList();
            List<(string, int, string)> pairs = new List<(string, int, string)>();


            string color;
            foreach(var item in res)
            {
                Course? course = context.Courses.FirstOrDefault(x => x.Id == item.CourseID);
                if (item.Degree < course.MinDegree)
                    color = "Red";
                else
                    color = "Green";

                pairs.Add((course.Name, item.Degree, color));
            }
            return View("TraineeShow", pairs);
            
        }
    }
}
