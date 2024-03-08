using Bulky.Models;
using BulkyBook.DataAccess.Repository;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> ls = _unitOfWork.Product.GetAll().ToList();
           

            return View(ls);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Product Added Successfully";
                return RedirectToAction("Index", "Product");

            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            Product? ProductDB = _unitOfWork.Product.Get(u => u.ProductId == id);// it only work on the primary key
                                                                                 //Product? ProductDB2 = _db.Categories.FirstOrDefault(u=>u.ProductId==id); // it works on any property
            if (id == null && id == 0 && ProductDB.ProductId == null)
            {
                return NotFound();
            }

            return View(ProductDB);

        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Product Updated Successfully";
                return RedirectToAction("Index", "Product");

            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            Product ProductDB = _unitOfWork.Product.Get(u => u.ProductId == id);
            if (id == null && id == 0 && ProductDB.ProductId == null)
            {
                return NotFound(); 
            }

            return View(ProductDB);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Product? ProductDB = _unitOfWork.Product.Get(u => u.ProductId == id);
            if (id == null && id == 0 && ProductDB == null)
            {
                return NotFound();
            }
            if (ProductDB != null)
            {
                _unitOfWork.Product.Remove(ProductDB);
                _unitOfWork.Save();
            }

            TempData["Success"] = "Product Deleted Successfully";
            return RedirectToAction("Index", "Product");


        }
    }
}
