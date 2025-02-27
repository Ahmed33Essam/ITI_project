using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI_project.Models
{
    public class Course
    {
        [Display(Name = "ID")]
        public int Id { set; get; }

        [Required]
        [MaxLength(20)]
        [MinLength(2)]
        [Unique]    
        public string Name { set; get; }

        [Required]
        [Range(50,100)]
        public int Degree { set; get; }

        [Display(Name = "Minimal Degree")]
        [Required]
        [Remote(action:"CheckMinDeg", controller:"Course", AdditionalFields = "Degree", ErrorMessage = "The Minimal Degree must be from 20:50")]
        public int MinDegree { set; get; }

        [Required]
        [Remote(action:"SetHours", controller:"Course", ErrorMessage = "The Hourse Must be less then 100")]
        public int Hours { set; get; }

        [Display(Name="Department")]
        [ForeignKey("Department")]
        [Range(1,int.MaxValue, ErrorMessage = "Plz make choose")]
        public int DeptID { set; get; }
        public Department? Department { set; get; }

        public List<CourseRes> CrsRes { set; get; } = new List<CourseRes>();
        public List<Instructor> Instructors { set; get; } = new List<Instructor>();
    }
}
