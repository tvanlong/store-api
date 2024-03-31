using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using storeAPI.Models;

namespace storeAPI.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category?> GetCategory(int id);
        Task<Category> AddCategory(Category category);
        Task<Category?> UpdateCategory(int id, Category category);
        Task<Category?> DeleteCategory(int id);
    }
}