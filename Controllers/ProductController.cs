using ProductCategory.Models;
using ProductCategory.Service;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ProductCategory.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public ProductController()
        {
            _productService = new ProductService(); ;
            _categoryService = new CategoryService();
        }

        // GET: Product
        // Here implemented server side pagination.
        public ActionResult Index(int page = 1)
        {
            int pageSize = 10;
            int totalRecords;
            var products = _productService.GetProducts(page, pageSize, out totalRecords);
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var model = new ProductListViewModel
            {
                Products = products,
                CurrentPage = page,
                TotalPages = totalPages,
            };
            return View(model);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_categoryService.GetAllCategories().ToList(), "CategoryId", "CategoryName");
            return View();
        }


        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_categoryService.GetAllCategories().ToList(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/id
        public ActionResult Edit(int id)
        {
            Product product = _productService.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_categoryService.GetAllCategories().ToList(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_categoryService.GetAllCategories().ToList(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/id
        public ActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        // POST: Product/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForever(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }   
    }
}