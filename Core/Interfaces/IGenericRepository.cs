using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
   public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetByID(object id);
        void Insert(T Entity);
        void Update(T Entity);
        void Delete(object id);
    }
}
