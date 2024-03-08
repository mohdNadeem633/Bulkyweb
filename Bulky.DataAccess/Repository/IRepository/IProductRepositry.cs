﻿using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;


namespace BulkyBook.DataAccess.Repository.IRepository
{

    public interface IProductRepository : IRepository<Product>
        {
            void Update(Product obj);

        }
   
}
