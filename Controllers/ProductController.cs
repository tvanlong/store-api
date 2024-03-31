using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using storeAPI.DTOs;
using storeAPI.Interfaces;
using storeAPI.Models;

namespace storeAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            var ProductsDTO = _mapper.Map<List<ProductDTO>>(products);
            return Ok(ProductsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDTO = _mapper.Map<ProductDTO>(product);
            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductDTO productDTO, List<IFormFile> files)
        {
            var product = _mapper.Map<Product>(productDTO);
            var newProduct = await _productRepository.AddProduct(product, files);
            var newProductDTO = _mapper.Map<ProductDTO>(newProduct);
            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProductDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDTO productDTO, List<IFormFile> files)
        {
            var product = _mapper.Map<Product>(productDTO);
            var updatedProduct = await _productRepository.UpdateProduct(id, product, files);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            var updatedProductDTO = _mapper.Map<ProductDTO>(updatedProduct);
            return Ok(updatedProductDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.DeleteProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}