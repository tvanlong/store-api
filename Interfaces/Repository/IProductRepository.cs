using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using storeAPI.Models;

namespace storeAPI.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product?> GetProduct(int id);
        Task<Product> AddProduct(Product product, List<IFormFile> files);
        Task<Product?> UpdateProduct(int id, Product product, List<IFormFile> files);
        Task<Product?> DeleteProduct(int id);
    }
}