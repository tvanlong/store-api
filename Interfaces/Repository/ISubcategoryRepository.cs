using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using storeAPI.Models;

namespace storeAPI.Interfaces
{
    public interface ISubcategoryRepository
    {
        Task<List<Subcategory>> GetSubcategories();
        Task<Subcategory?> GetSubcategory(int id);
        Task<Subcategory> AddSubcategory(Subcategory subcategory);
        Task<Subcategory?> UpdateSubcategory(int id, Subcategory subcategory);
        Task<Subcategory?> DeleteSubcategory(int id);
    }
}