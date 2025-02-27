using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI_project.Models
{
    public class CourseRes
    {
        public int Id { set; get; }
        public int Degree { set; get; }

        [ForeignKey("Trainee")]
        [Required]
        [Display(Name = "Trainee ID")]
        [Remote(action:"ISTraineeIDCorrect", controller:"CourseRes", ErrorMessage = "There is no Trainee with this ID")]
        public int TraineeID { set; get; }
        public Trainee? Trainee { set; get; }

        [ForeignKey("Course")]
        [Required]
        [Display(Name = "Course ID")]
        public int CourseID { set; get; }
        public Course? Course { set; get; }
    }
}
