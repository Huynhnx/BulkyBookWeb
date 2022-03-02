using BulkyBook.DataAccess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _Db;
        public CategoryController(ApplicationDBContext Db)
        {
            _Db = Db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategory = _Db.Categories.ToList();
            return View(objCategory);
        }
        //Get
        public IActionResult Create()
        {
           
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (_Db.Categories.FirstOrDefault(x=>x.DisplayOrder== obj.DisplayOrder) != null)
            {
                ModelState.AddModelError("DisplayOrder", "Display Order has been exist in list");
            }
            if (ModelState.IsValid)
            {
                _Db.Categories.Add(obj);
                _Db.SaveChanges();
                TempData.Add("Success", "Create Successfully");
                return RedirectToAction("Index");
            }
           return View();
        }
        //Get
        public IActionResult Edit(int? id)
        {
            if (id== null || id ==0)
            {
                return NotFound();
            }
            var category = _Db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                _Db.Categories.Update(obj);
                _Db.SaveChanges();
                TempData.Add("Success", "Edit Successfully");
                return RedirectToAction("Index");
            }
            return View();
        }
        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _Db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? id)
        {
            var category = _Db.Categories.Find(id);
            if(category != null)
            {
                _Db.Categories.Remove(category);
                _Db.SaveChanges();
                TempData.Add("Success", "Delete Category Successfully");
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
