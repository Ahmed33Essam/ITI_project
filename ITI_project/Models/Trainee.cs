using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI_project.Models
{
    public class Trainee
    {
        [Display(Name = "Trainee ID")]
        [Required]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        public string Img { set; get; }
        public string? Address { set; get; }
        [Required]
        public int Grad { set; get; }

        [ForeignKey("Department")]
        public int DeptID { set; get; }
        public Department Department { set; get; }

        public List<CourseRes> CrsRes { set; get; } = new List<CourseRes>();

    }
}
