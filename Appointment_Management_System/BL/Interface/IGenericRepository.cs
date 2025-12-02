namespace Appointment_Management_System.BL.Interface
{
    public interface IGenericRepository<T> :IDisposable
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        Task Remove(int id);
        Task Delete(int id);
        IQueryable<T> Query();
        Task<int> SaveAsync();  //Temp Save we can UOW
    }
}
