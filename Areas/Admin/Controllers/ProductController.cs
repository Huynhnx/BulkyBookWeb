using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork Db, IWebHostEnvironment hostEnv)
        {
            _unitOfWork = Db;
            _hostEnvironment= hostEnv;
        }
        public IActionResult Index()
        {
            //IEnumerable<Product> objProduct = _unitOfWork.Product.GetAll();
            return View();
        }
       
        //Get
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM= new()
            {
                product = new(),
                categories = _unitOfWork.Category.GetAll().Select(x=> new SelectListItem{
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                covertypes = _unitOfWork.CoverType.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            if (id== null || id ==0)
            {
                
                return View(productVM);
            }
            else
            {
                
            }
            
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string FileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath,@"images\products");
                    var extention = Path.GetExtension(wwwRootPath).ToLower();
                    using (var fileStreams = new FileStream(Path.Combine(uploads,FileName+ extention),FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.product.ImageUrl = @"images\products" + FileName + extention;
                }
                _unitOfWork.Product.Add(obj.product);
                _unitOfWork.Save();
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
            var product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int? id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _unitOfWork.Product.Remove(product);
                _unitOfWork.Save();
                TempData.Add("Success", "Delete Product Successfully");
                return RedirectToAction("Index");
            }
            return View();
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll();
            return Json(new { data = productList });
        }
        #endregion
    }
}
