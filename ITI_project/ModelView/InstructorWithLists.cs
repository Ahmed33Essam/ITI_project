using ITI_project.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI_project.ModelView
{
    public class InstructorWithLists
    {
        public ListOfCoursesAndDepartments lists = new ListOfCoursesAndDepartments();
        public int Id { set; get; }
        public string Name { set; get; }
        public string Img { set; get; }
        public string? Address { set; get; }
        public int Salary { set; get; }
        public int DeptID { set; get; }
        public int CourseID { set; get; }
    }
}
