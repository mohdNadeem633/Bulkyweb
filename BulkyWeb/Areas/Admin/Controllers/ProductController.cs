using Bulky.Models;
using BulkyBook.DataAccess.Repository;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> products = (List<Product>)_unitOfWork.Product.GetAll();
            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            var productVM = new ProductVM
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString(),

                }),
                Product = id == null ? new Product() : _unitOfWork.Product.Get(u => u.ProductId == id)
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, "images", "product");
                    string filePath = Path.Combine(productPath, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = "/images/product/" + fileName;
                }

                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();

                TempData["Success"] = "Product Added Successfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString(),
                });

                return View(productVM);
            }
        }

        public IActionResult Delete(int? id)
        {
            Product product = _unitOfWork.Product.Get(u => u.ProductId == id);
            if (id == null || product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product product = _unitOfWork.Product.Get(u => u.ProductId == id);
            if (id == null || product == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();

            TempData["Success"] = "Product Deleted Successfully";
            return RedirectToAction("Index", "Product");
        }
    }
}
