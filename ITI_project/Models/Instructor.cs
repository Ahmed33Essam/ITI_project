using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI_project.Models
{
    public class Instructor
    {
        public int Id { set; get; } 
        public string Name { set; get; }
        public string? Address { set; get; }
        public int Salary { set; get; }

        //[RegularExpression(@"\w+\.(png|jpg)", ErrorMessage = "the Img must be png or jpg")]
        public string? Img { get; set; }

        [Display(Name = "Upload File")]
        [NotMapped]
        public IFormFile? Image { get; set; }


        [ForeignKey("Department")]
        public int DeptID { set; get; }
        public Department Department { set; get; }

        [ForeignKey("Course")]
        public int CourseID { set; get; }
        public Course Course { set; get; }
    }
}
