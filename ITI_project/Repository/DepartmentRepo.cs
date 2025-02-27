using ITI_project.Models;

namespace ITI_project.Repository
{
    public class DepartmentRepo : IGenRepository<Department>
    {
        Context context;
        public DepartmentRepo(Context _context)
        {
            context = _context;
            Id = Guid.NewGuid().ToString();
        }

        public string Id { set; get; }

        public void Add(Department obj)
        {
            context.Add(obj);
        }

        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }

        public Department GetByID(int id)
        {
            return context.Departments.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            context.Remove(GetByID(id));
        }

        public void Update(Department obj)
        {
            context.Update(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
