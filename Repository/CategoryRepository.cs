using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using storeAPI.Data;
using storeAPI.Interfaces;
using storeAPI.Models;

namespace storeAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;
        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return null;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories
                .Include(c => c.Subcategories)
                .ThenInclude(s => s.Products)
                .ThenInclude(p => p.Versions)
                .ToListAsync();
        }

        public async Task<Category?> GetCategory(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Subcategories)
                .ThenInclude(s => s.Products)
                .ThenInclude(p => p.Versions)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return null;
            }
            return category;
        }

        public async Task<Category?> UpdateCategory(int id, Category category)
        {
            var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (categoryToUpdate == null)
            {
                return null;
            }
            categoryToUpdate.Name = category.Name;
            await _context.SaveChangesAsync();
            return categoryToUpdate;
        }
    }
}