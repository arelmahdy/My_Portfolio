using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork
{
   public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DataContext _context;
        private  IGenericRepository<T> _entity;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public IGenericRepository<T> Entity
        { 
            get
            {
                return _entity ?? (_entity = new GenericRepositry<T>(_context));
            } 
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
