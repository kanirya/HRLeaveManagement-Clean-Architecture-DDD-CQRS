using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Add(T entity);
        Task<bool> Exists(int id);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
    }
}
