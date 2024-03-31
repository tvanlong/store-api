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
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);
            return Ok(categoriesDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            var category = await _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            var newCategory = await _categoryRepository.AddCategory(category);
            var newCategoryDTO = _mapper.Map<CategoryDTO>(newCategory);
            return CreatedAtAction(nameof(GetCategory), new { id = newCategoryDTO.Id }, newCategoryDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            var updatedCategory = await _categoryRepository.UpdateCategory(id, category);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            var updatedCategoryDTO = _mapper.Map<CategoryDTO>(updatedCategory);
            return Ok(updatedCategoryDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var category = await _categoryRepository.DeleteCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}