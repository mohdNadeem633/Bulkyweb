using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> ls = _db.Categories.ToList();

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
				_db.Categories.Add(obj);
				_db.SaveChanges();
				TempData["Success"] = "Category Added Successfully";
				return RedirectToAction("Index", "Category");

			}
            return View();

		}

		public IActionResult Edit(int? id)
		{
			Category? categoryDB = _db.Categories.Find(id);// it only work on the primary key
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
				_db.Categories.Update(obj);
				_db.SaveChanges();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToAction("Index", "Category");

			}
		
			return View();

		}

		public IActionResult Delete(int? id)
        {
            Category? categoryDB = _db.Categories.Find(id);

            if (id == null && id == 0 && categoryDB == null)
            {
                return NotFound();
            }
		
            return View(categoryDB);
        }

		[HttpPost,ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
            Category? categoryDB = _db.Categories.Find(id);
            if (id == null && id == 0 && categoryDB == null)
            {
                return NotFound();
            }
			if(categoryDB != null)
			{
				_db.Categories.Remove(categoryDB);
				_db.SaveChanges();
			}

            TempData["Success"] = "Category Deleted Successfully";
            return RedirectToAction("Index","Category");
		}
       

	}
}
