using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
	[BindProperties]
	public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
       
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
             


        }
        public IActionResult OnPost(Category obj) {
            _db.Categories.Add(Category);
            _db.SaveChanges();
			TempData["Success"] = "Category Created Successfully";
			return RedirectToPage("Index");
            
        
        
        }

  //      public IActionResult OnPost(int id)
  //      {
  //          var d = _db.Categories.Find(id);
  //          _db.Categories.Remove(d);
  //          _db.SaveChanges();

		//	return RedirectToPage("Index");
		//}



    }
}
