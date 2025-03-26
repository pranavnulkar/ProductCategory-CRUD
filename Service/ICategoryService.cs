using ProductCategory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCategory.Service
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
        Category GetCategoryById(int categoryId);
        List<Category> GetAllCategories();
    }
}
