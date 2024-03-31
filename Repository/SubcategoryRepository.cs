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
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly ApplicationDBContext _context;
        public SubcategoryRepository(ApplicationDBContext context) {
            _context = context;
        }

        public async Task<Subcategory> AddSubcategory(Subcategory subcategory)
        {
            await _context.Subcategories.AddAsync(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }

        public async Task<Subcategory?> DeleteSubcategory(int id)
        {
            var subcategory = await _context.Subcategories.FirstOrDefaultAsync(x => x.Id == id);
            if(subcategory == null) return null;
            _context.Subcategories.Remove(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }

        public async Task<List<Subcategory>> GetSubcategories()
        {
            return await _context.Subcategories
                .Include(s => s.Products)
                .ToListAsync();
        }

        public async Task<Subcategory?> GetSubcategory(int id)
        {
            return await _context.Subcategories
                .Include(s => s.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Subcategory?> UpdateSubcategory(int id, Subcategory subcategory)
        {
            var subcategoryToUpdate = await _context.Subcategories.FirstOrDefaultAsync(x => x.Id == id);
            if(subcategoryToUpdate == null) return null;
            subcategoryToUpdate.Name = subcategory.Name;
            subcategoryToUpdate.CategoryId = subcategory.CategoryId;
            await _context.SaveChangesAsync();
            return subcategoryToUpdate;
        }
    }
}