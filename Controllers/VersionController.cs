using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using storeAPI.DTOs;
using storeAPI.Interfaces;
using storeAPI.Models;
using Version = storeAPI.Models.Version;

namespace storeAPI.Controllers
{
    [Route("api/version")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly IVersionRepository _versionRepository;
        private readonly IMapper _mapper;
        public VersionController(IVersionRepository versionRepository, IMapper mapper)
        {
            _versionRepository = versionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetVersions()
        {
            var versions = await _versionRepository.GetVersions();
            var versionDtos = _mapper.Map<List<VersionDTO>>(versions);
            return Ok(versionDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVersion(int id)
        {
            var version = await _versionRepository.GetVersion(id);
            if (version == null)
            {
                return NotFound();
            }
            var versionDto = _mapper.Map<VersionDTO>(version);
            return Ok(versionDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddVersion([FromBody] VersionDTO versionDto)
        {
            var version = _mapper.Map<Version>(versionDto);
            var addedVersion = await _versionRepository.AddVersion(version);
            var addedVersionDto = _mapper.Map<VersionDTO>(addedVersion);
            return CreatedAtAction(nameof(GetVersion), new { id = addedVersion.Id }, addedVersionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVersion(int id, [FromBody] VersionDTO versionDto)
        {
            var version = _mapper.Map<Version>(versionDto);
            var updatedVersion = await _versionRepository.UpdateVersion(id, version);
            if (updatedVersion == null)
            {
                return NotFound();
            }
            var updatedVersionDto = _mapper.Map<VersionDTO>(updatedVersion);
            return Ok(updatedVersionDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVersion(int id)
        {
            var deletedVersion = await _versionRepository.DeleteVersion(id);
            if (deletedVersion == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}