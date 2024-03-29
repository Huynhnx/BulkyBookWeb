﻿using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _IUnitOfWork;
        public CategoryController(IUnitOfWork Db)
        {
            _IUnitOfWork = Db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategory = _IUnitOfWork.Category.GetAll();
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
            if (_IUnitOfWork.Category.GetFirstOrDefault(x=>x.DisplayOrder== obj.DisplayOrder) != null)
            {
                ModelState.AddModelError("DisplayOrder", "Display Order has been exist in list");
            }
            if (ModelState.IsValid)
            {
                _IUnitOfWork.Category.Add(obj);
                _IUnitOfWork.Save();
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
            var category = _IUnitOfWork.Category.GetFirstOrDefault(x=>x.Id==id);
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
                _IUnitOfWork.Category.Update(obj);
                _IUnitOfWork.Save();
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
            var category = _IUnitOfWork.Category.GetFirstOrDefault(x=>x.Id==id);
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
            var category = _IUnitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                _IUnitOfWork.Category.Remove(category);
                _IUnitOfWork.Save();
                TempData.Add("Success", "Delete Category Successfully");
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
