using Bulky.Models;
using BulkyBook.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> ls = _unitOfWork.Category.GetAll().ToList();

            return View(ls);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category Added Successfully";
                return RedirectToAction("Index", "Category");

            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            Category? categoryDB = _unitOfWork.Category.Get(u => u.CategoryId == id);// it only work on the primary key
                                                                                     //Category? categoryDB2 = _db.Categories.FirstOrDefault(u=>u.CategoryId==id); // it works on any property
                                                                                     //Category? categoryDB3 = _db.Categories.Where(u=>u.CategoryId==id).FirstOrDefault(); // use for the filltering

            if (id == null && id == 0 && categoryDB == null)
            {
                return NotFound();
            }


            return View(categoryDB);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToAction("Index", "Category");

            }

            return View();

        }

        public IActionResult Delete(int? id)
        {
            Category? categoryDB = _unitOfWork.Category.Get(u => u.CategoryId == id);

            if (id == null && id == 0 && categoryDB == null)
            {
                return NotFound();
            }

            return View(categoryDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? categoryDB = _unitOfWork.Category.Get(u => u.CategoryId == id);
            if (id == null && id == 0 && categoryDB == null)
            {
                return NotFound();
            }
            if (categoryDB != null)
            {
                _unitOfWork.Category.Remove(categoryDB);
                _unitOfWork.Save();
            }

            TempData["Success"] = "Category Deleted Successfully";
            return RedirectToAction("Index", "Category");
        }



    }
}
