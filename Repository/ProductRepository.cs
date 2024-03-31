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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;
        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProduct(Product product, List<IFormFile> files)
        {
            List<string> imagePaths = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var folderName = Path.Combine("wwwroot", "uploads", "images", "products");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    imagePaths.Add("uploads/images/products/" + fileName);
                }
            }

            product.Images = string.Join(",", imagePaths);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return null;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> GetProduct(int id)
        {
            return await _context.Products
                .Include(p => p.Versions)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products
                .Include(p => p.Versions)
                .ToListAsync();
        }

        public async Task<Product?> UpdateProduct(int id, Product product, List<IFormFile> files)
        {
            var productToUpdate = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (productToUpdate == null)
            {
                return null;
            }
            productToUpdate.Name = product.Name;
            productToUpdate.SubcategoryId = product.SubcategoryId;
           
            if (files.Count > 0)
            {
                List<string> imagePaths = new List<string>();

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var folderName = Path.Combine("wwwroot", "uploads", "images", "products");
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                        var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        var fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        imagePaths.Add("uploads/images/products/" + fileName);
                    }
                }

                productToUpdate.Images = string.Join(",", imagePaths);
            }

            await _context.SaveChangesAsync();
            return productToUpdate;
        }
    }
}