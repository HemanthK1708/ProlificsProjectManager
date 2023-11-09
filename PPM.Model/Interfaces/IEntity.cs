namespace PPM.Model
{
    public interface IEntity<T>
    {
        public void Add(T classObject);

        public List<T> ViewAll();

        public T ViewByID(int Id);

        public void DeleteByID(int Id);

    }
}