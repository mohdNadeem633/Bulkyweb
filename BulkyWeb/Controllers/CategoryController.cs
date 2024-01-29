using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _CategoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _CategoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category> ls = _CategoryRepo.GetAll().ToList();

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
                _CategoryRepo.Add(obj);
                _CategoryRepo.Save();
				TempData["Success"] = "Category Added Successfully";
				return RedirectToAction("Index", "Category");

			}
            return View();

		}

		public IActionResult Edit(int? id)
		{
			Category? categoryDB = _CategoryRepo.Get(u=>u.CategoryId==id);// it only work on the primary key
			//Category? categoryDB2 = _db.Categories.FirstOrDefault(u=>u.CategoryId==id); // it works on any property
			//Category? categoryDB3 = _db.Categories.Where(u=>u.CategoryId==id).FirstOrDefault(); // use for the filltering

			if (id == null && id == 0 && categoryDB==null)
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
                _CategoryRepo.Update(obj);
                _CategoryRepo.Save();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToAction("Index", "Category");

			}
		
			return View();

		}

		public IActionResult Delete(int? id)
        {
            Category? categoryDB = _CategoryRepo.Get(u=>u.CategoryId==id);

            if (id == null && id == 0 && categoryDB == null)
            {
                return NotFound();
            }
		
            return View(categoryDB);
        }

		[HttpPost,ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
            Category? categoryDB = _CategoryRepo.Get(u => u.CategoryId == id);
            if (id == null && id == 0 && categoryDB == null)
            {
                return NotFound();
            }
			if(categoryDB != null)
			{
                _CategoryRepo.Remove(categoryDB);
                _CategoryRepo.Save();
			}

            TempData["Success"] = "Category Deleted Successfully";
            return RedirectToAction("Index","Category");
		}
       

	}
}
