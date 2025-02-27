using ITI_project.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_project.Repository
{
    public class CourseRepo : IGenRepository<Course>
    {
        Context context;
        public CourseRepo(Context _context)
        {
            context = _context;
            Id = Guid.NewGuid().ToString();
        }

        public string Id { set; get; }

        public void Add(Course obj)
        {
            context.Add(obj);
        }

        public List<Course> GetAll()
        {
            return context.Courses.Include(x => x.Department).ToList();
        }

        public Course GetByID(int id)
        {
            return context.Courses.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            context.Remove(GetByID(id));
        }

        public void Update(Course obj)
        {
            context.Update(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
