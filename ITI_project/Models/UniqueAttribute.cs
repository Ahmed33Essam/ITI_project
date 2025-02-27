using System.ComponentModel.DataAnnotations;

namespace ITI_project.Models
{
    public class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return null;
            
            string Name = value.ToString();

            Context context = new Context();
            
            Course course = context.Courses.FirstOrDefault(x => x.Name == Name);

            if (course == null)
                return ValidationResult.Success;

            Course CurrentCourse = (Course)validationContext.ObjectInstance;

            if (CurrentCourse.Id == course.Id)
                return ValidationResult.Success;
            return new ValidationResult("The name is used ");
            
        }
    }
}
