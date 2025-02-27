namespace ITI_project.Repository
{
    public interface IGenRepository<T>
    {
        public string Id { set; get; }


        public void Add(T obj);
        public void Remove(int id);
        public void Update(T obj);
        public T GetByID(int id);
        public List<T> GetAll();
        public void Save();
    }
}
