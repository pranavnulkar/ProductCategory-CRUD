using ProductCategory.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProductCategory.Models
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}