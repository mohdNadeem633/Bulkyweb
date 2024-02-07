using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
       
        public CategoryRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
            // it's equalent _db.Categories==dbSet
        }

       

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
