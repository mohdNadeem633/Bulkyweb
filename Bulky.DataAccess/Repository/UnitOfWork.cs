using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            // it's equalent _db.Categories==dbSet
            Category  = new CategoryRepository(_db);

        }

        void IUnitOfWork.Save()
        {
            _db.SaveChanges();
        }
    }
}
