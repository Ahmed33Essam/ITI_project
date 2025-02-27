namespace ITI_project.Models
{
    public class Department
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string? Manager { set; get; }

        public List<Instructor> Instructors { set; get; } = new List<Instructor>();
        public List<Course> Courses { set; get; } = new List<Course>();
        public List<Trainee> Trainees { set; get; } = new List<Trainee>();
    }
}
