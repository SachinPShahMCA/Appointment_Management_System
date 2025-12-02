using Appointment_Management_System.BL.Interface;
using Appointment_Management_System.Data;
using Appointment_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Appointment_Management_System.BL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        private bool _disposed = false; // Track whether Dispose has been called

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }


        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            //return await _dbSet.FindAsync(id);
            return await _dbSet.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public virtual async Task Remove(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public virtual async Task Delete (int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is BaseEntity baseentity)
            {
                baseentity.IsDeleted = true;
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
       
        public virtual void Update(T entity)
        {
            //_dbSet.Update(entity);
            _dbSet.Attach(entity);                         // attach manually
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual IQueryable<T> Query() => _dbSet.AsQueryable();

        // Dispose pattern implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    _context.Dispose();
                }
                // Dispose unmanaged resources (if any) here
                _disposed = true;
            }
        }
        
        ~GenericRepository()
        {
            Dispose(false);
        }
    }
}

