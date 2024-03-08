using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.Models;



namespace BulkyBook.DataAccess.Repository.IRepository
{

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            // it's equalent _db.Categories==dbSet
        }


        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }

       
    }
}
