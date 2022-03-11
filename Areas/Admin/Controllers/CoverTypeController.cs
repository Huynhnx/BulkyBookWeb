using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _Db;
        public CoverTypeController(IUnitOfWork Db)
        {
            _Db = Db;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverType = _Db.CoverType.GetAll();
            return View(objCoverType);
        }
        //Get
        public IActionResult Create()
        {
           
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            
            if (ModelState.IsValid)
            {
                _Db.CoverType.Add(obj);
                _Db.Save();
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
            var covertype = _Db.CoverType.GetFirstOrDefault(x=>x.Id==id);
            if (covertype == null)
            {
                return NotFound();
            }
            return View(covertype);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            
            if (ModelState.IsValid)
            {
                _Db.CoverType.Update(obj);
                _Db.Save();
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
            var covertype = _Db.CoverType.GetFirstOrDefault(x => x.Id == id);
            if (covertype == null)
            {
                return NotFound();
            }
            return View(covertype);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCoverType(int? id)
        {
            var covertype = _Db.CoverType.GetFirstOrDefault(x => x.Id == id);
            if (covertype != null)
            {
                _Db.CoverType.Remove(covertype);
                _Db.Save();
                TempData.Add("Success", "Delete Cover Type Successfully");
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
