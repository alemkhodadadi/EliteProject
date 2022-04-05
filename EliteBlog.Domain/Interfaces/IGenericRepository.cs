using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EliteBlog.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}