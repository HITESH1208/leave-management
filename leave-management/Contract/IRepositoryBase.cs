using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Contract
{
    public interface IRepositoryBase<T> where T: class
    {
        ICollection<T> FindAll();
        T FindById(int id);
        bool Create(T Entity);
        bool Update(T Entity);
        bool Delete(T Entity);
        bool Save();

    }
}
