using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repository
{
    public class GenericRepositry<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private DbSet<T> table = null;
        public GenericRepositry(DataContext Context)
        {
            _context = Context;
            table = _context.Set<T>();
        }
        public void Delete(object id)
        {
            T existing = GetByID(id);
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetByID(object id)
        {
            return table.Find(id);
        }

        public void Insert(T Entity)
        {
            table.Add(Entity);
        }

        public void Update(T Entity)
        {
            table.Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;
        }
    }
}
