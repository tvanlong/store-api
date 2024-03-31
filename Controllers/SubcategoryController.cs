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
    [Route("api/subcategory")]
    [ApiController]
    public class SubcategoryController : ControllerBase
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IMapper _mapper;
        public SubcategoryController(ISubcategoryRepository subcategoryRepository, IMapper mapper)
        {
            _subcategoryRepository = subcategoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubcategories()
        {
            var subcategories = await _subcategoryRepository.GetSubcategories();
            var subcategoriesDTO = _mapper.Map<List<SubcategoryDTO>>(subcategories);
            return Ok(subcategoriesDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubcategory(int id)
        {
            var subcategory = await _subcategoryRepository.GetSubcategory(id);
            if(subcategory == null) return NotFound();
            var subcategoryDTO = _mapper.Map<SubcategoryDTO>(subcategory);
            return Ok(subcategoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubcategory([FromBody] SubcategoryDTO subcategoryDTO)
        {
            var subcategory = _mapper.Map<Subcategory>(subcategoryDTO);
            var addedSubcategory = await _subcategoryRepository.AddSubcategory(subcategory);
            var addedSubcategoryDTO = _mapper.Map<SubcategoryDTO>(addedSubcategory);
            return CreatedAtAction(nameof(GetSubcategory), new { id = addedSubcategoryDTO.Id }, addedSubcategoryDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubcategory(int id, [FromBody] SubcategoryDTO subcategoryDTO)
        {
            var subcategory = _mapper.Map<Subcategory>(subcategoryDTO);
            var updatedSubcategory = await _subcategoryRepository.UpdateSubcategory(id, subcategory);
            if(updatedSubcategory == null) return NotFound();
            var updatedSubcategoryDTO = _mapper.Map<SubcategoryDTO>(updatedSubcategory);
            return Ok(updatedSubcategoryDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubcategory(int id)
        {
            var deletedSubcategory = await _subcategoryRepository.DeleteSubcategory(id);
            if(deletedSubcategory == null) return NotFound();
            return NoContent();
        }
    }
}