using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using storeAPI.Data;
using storeAPI.Interfaces;
using Version = storeAPI.Models.Version;


namespace storeAPI.Repository
{
    public class VersionRepository : IVersionRepository
    {
        private readonly ApplicationDBContext _context;
        public VersionRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Version> AddVersion(Version version)
        {
            await _context.Versions.AddAsync(version);
            await _context.SaveChangesAsync();
            return version;
        }

        public async Task<Version?> DeleteVersion(int id)
        {
            var version = await _context.Versions.FirstOrDefaultAsync(x => x.Id == id);
            if (version == null)
            {
                return null;
            }
            _context.Versions.Remove(version);
            await _context.SaveChangesAsync();
            return version;
        }

        public async Task<Version?> GetVersion(int id)
        {
            return await _context.Versions 
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Version>> GetVersions()
        {
            return await _context.Versions
                .ToListAsync();
        }

        public async Task<Version?> UpdateVersion(int id, Version version)
        {
            var versionToUpdate = await _context.Versions.FirstOrDefaultAsync(x => x.Id == id);
            if (versionToUpdate == null)
            {
                return null;
            }
            versionToUpdate.Name = version.Name;
            versionToUpdate.CurrentPrice = version.CurrentPrice;
            versionToUpdate.OldPrice = version.OldPrice;
            versionToUpdate.Status = version.Status;
            versionToUpdate.Details = version.Details;
            versionToUpdate.ProductId = version.ProductId;
            await _context.SaveChangesAsync();
            return versionToUpdate;
        }
    }
}