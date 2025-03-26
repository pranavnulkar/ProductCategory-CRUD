using ProductCategory.Models;
using ProductCategory.Service;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProductCategory.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public CategoryController()
        {
            _categoryService = new CategoryService();
        }

        public ActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }


        // GET: Category/Edit/id
        public ActionResult Edit(int id) 
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();                
            }
            return View(category);
        }

        // POST: Category/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid) 
            {
                _categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Category/Delete/id
        public ActionResult Delete(int id)
        {           
            var category= _categoryService.GetCategoryById(id);
            if(category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Delete/id
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}