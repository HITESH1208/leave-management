using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contract
{
    public interface IRepositoryBase<T> where T: class
    {
        Task<ICollection<T>> FindAll();
        Task<T> FindById(int id);
        Task<bool> Create(T Entity);
        Task<bool> Update(T Entity);
        Task<bool> Delete(T Entity);
        Task<bool> Save();

    }
}
