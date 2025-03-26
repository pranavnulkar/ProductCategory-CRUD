using ProductCategory.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProductCategory.Service
{
    public class ProductService:IProductService
    {
        private readonly ProductCategoryContext _context;
        public ProductService()
        {
            _context = new ProductCategoryContext();
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            Product product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            } 
        }

        public Product GetProductById(int  productId)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(p=>p.ProductId==productId);
        }

        public List<Product> GetProducts(int page, int pageSize, out int totalRecords)
        {
            var query = _context.Products.Include("Category");
            totalRecords=query.Count();

            return query
                .OrderBy(p => p.ProductId).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();            
        }
    }
}