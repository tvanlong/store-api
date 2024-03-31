using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using storeAPI.Data;
using Microsoft.EntityFrameworkCore;
using Version = storeAPI.Models.Version;
using storeAPI.DTOs;
using storeAPI.Models;
using storeAPI.DTOs.ProductVersionDTOs;
using storeAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace storeAPI.Controllers
{
    [Route("api/product-version")]
    [ApiController]
    public class ProductVersionController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IVersionRepository _versionRepository;
        private readonly IProductRepository _productRepository;
        public ProductVersionController(ApplicationDBContext context, IVersionRepository versionRepository, IProductRepository productRepository)
        {
            _context = context;
            _versionRepository = versionRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAllProductVersions()
        {
            var result = await _context.Versions
                .Include(v => v.Product)
                .ThenInclude(p => p.Subcategory)
                .ThenInclude(s => s.Category)
                .Select(v => new ResponseProductVersionDTO
                {
                    Id = v.Id,
                    Images = v.Product.Images,
                    ProductName = v.Product.Name,
                    VersionName = v.Name,
                    ProductId = v.ProductId,
                    OldPrice = v.OldPrice,
                    CurrentPrice = v.CurrentPrice,
                    Status = v.Status,
                    Details = v.Details,
                    SubcategoryId = v.Product.SubcategoryId,
                    SubcategoryName = v.Product.Subcategory.Name,
                    CategoryId = v.Product.Subcategory.CategoryId,
                    CategoryName = v.Product.Subcategory.Category.Name
                })
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductVersion(int id)
        {
            var result = await _context.Versions
                .Include(v => v.Product)
                .ThenInclude(p => p.Subcategory)
                .ThenInclude(s => s.Category)
                .Select(v => new ResponseProductVersionDTO
                {
                    Id = v.Id,
                    Images = v.Product.Images,
                    ProductName = v.Product.Name,
                    VersionName = v.Name,
                    ProductId = v.ProductId,
                    OldPrice = v.OldPrice,
                    CurrentPrice= v.CurrentPrice,
                    Status= v.Status,
                    Details = v.Details,
                    SubcategoryId = v.Product.SubcategoryId,
                    SubcategoryName = v.Product.Subcategory.Name,
                    CategoryId = v.Product.Subcategory.CategoryId,
                    CategoryName = v.Product.Subcategory.Category.Name
                })
                .FirstOrDefaultAsync(v => v.Id == id);
            return Ok(result);
        }

        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> AddNewProductVersion([FromForm] CreateProductVersionDTO createDTO, List<IFormFile> files)
        {
            var product = new Product{
                Name = createDTO.ProductName,
                Images = createDTO.ProductImages,
                SubcategoryId = createDTO.SubcategoryId
            };
            var productResult = await _productRepository.AddProduct(product, files);

            var version = new Version{
                Name = createDTO.VersionName,
                OldPrice = createDTO.OldPrice,
                CurrentPrice = createDTO.CurrentPrice,
                Status = createDTO.Status,
                Details = createDTO.Details,
                ProductId = product.Id
            };
            var versionResult = await _versionRepository.AddVersion(version);

            var subcategory = await _context.Subcategories.FirstOrDefaultAsync(s => s.Id == productResult.SubcategoryId);
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == subcategory.CategoryId);

            var result = new ResponseProductVersionDTO {
                Id = versionResult.Id,
                Images = productResult.Images,
                ProductName = productResult.Name,
                VersionName = versionResult.Name,
                ProductId = productResult.Id,
                OldPrice = versionResult.OldPrice,
                CurrentPrice= versionResult.CurrentPrice,
                Status = versionResult.Status,
                Details = versionResult.Details,
                SubcategoryId = subcategory.Id,
                SubcategoryName = subcategory.Name,
                CategoryId = category.Id,
                CategoryName = category.Name
            };
            return CreatedAtAction(nameof(GetProductVersion), new { id = versionResult.Id }, result);
        }

        [HttpPost]
        [Route("version")]
        public async Task<IActionResult> AddVersionForProduct(CreateVersionDTO createVersionDTO)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == createVersionDTO.ProductId);
            var subcategory = await _context.Subcategories.FirstOrDefaultAsync(s => s.Id == product.SubcategoryId);
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == subcategory.CategoryId);

            var version = new Version{
                Name = createVersionDTO.VersionName,
                OldPrice = createVersionDTO.OldPrice,
                CurrentPrice = createVersionDTO.CurrentPrice,
                Status = createVersionDTO.Status,
                Details = createVersionDTO.Details,
                ProductId = createVersionDTO.ProductId
            };
            var versionResult = await _versionRepository.AddVersion(version);

            var result = new ResponseProductVersionDTO {
                Id = versionResult.Id,
                Images = product.Images,
                ProductName = product.Name,
                VersionName = versionResult.Name,
                ProductId = product.Id,
                OldPrice = versionResult.OldPrice,
                CurrentPrice = versionResult.CurrentPrice,
                Status = versionResult.Status,
                Details = versionResult.Details,
                SubcategoryId = subcategory.Id,
                SubcategoryName = subcategory.Name,
                CategoryId = category.Id,
                CategoryName = category.Name
            };
            return CreatedAtAction(nameof(GetProductVersion), new { id = versionResult.Id }, result);
        }
    }
}