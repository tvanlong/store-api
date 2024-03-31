using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version = storeAPI.Models.Version;

namespace storeAPI.Interfaces
{
    public interface IVersionRepository
    {
        Task<List<Version>> GetVersions();
        Task<Version?> GetVersion(int id);
        Task<Version> AddVersion(Version version);
        Task<Version?> UpdateVersion(int id, Version version);
        Task<Version?> DeleteVersion(int id);
    }
}