using ProductCategory.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Net;

namespace ProductCategory.Controllers
{
    public class ProductController : Controller
    {
        private ProductCategoryContext _context = new ProductCategoryContext();

        // GET: Product
        // Here implemented server side pagination.
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var products = _context.Products.OrderBy(p => p.ProductId).Include(p=>p.Category).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
            //.Select(p => new
            //    {
            //        p.ProductId,
            //        p.ProductName,
            //        p.CategoryId,
            //        CategoryName = p.Category.CategoryName
            //    }).ToList();

            // Total count for pagination controls (could be sent via ViewBag or a view model)
            int totalRecords = _context.Products.Count();

            //view bag to pass data and paging information.
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = totalRecords;
            return View(products);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            using(var db=new ProductCategoryContext())
            {
                ViewBag.CategoryId = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            }
            return View();
        }


        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _context.Products.Find(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        // POST: Product/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForever(int? id)
        {
            Product product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();  //close database connection and free the resources
            base.Dispose(disposing);
        }
    }
}