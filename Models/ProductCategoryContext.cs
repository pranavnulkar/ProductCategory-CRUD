using System.Data.Entity;

namespace ProductCategory.Models
{
    public class ProductCategoryContext:DbContext
    {
        public ProductCategoryContext() : base("ProductCategoryDBCS")
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}