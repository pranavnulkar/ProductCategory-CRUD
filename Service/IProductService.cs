using ProductCategory.Models;
using System.Collections.Generic;

namespace ProductCategory.Service
{
    public interface IProductService
    {
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
        Product GetProductById(int productId);
        List<Product> GetProducts(int page, int pageSize, out int totalRecords);
    }
}
