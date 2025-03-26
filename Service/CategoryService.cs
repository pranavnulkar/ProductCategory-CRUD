using ProductCategory.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProductCategory.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly ProductCategoryContext _context;
        public CategoryService()
        {
            _context=new ProductCategoryContext();
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if(category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.Find(categoryId);
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
    }
}