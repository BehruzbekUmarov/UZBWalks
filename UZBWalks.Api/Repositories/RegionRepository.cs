using Microsoft.EntityFrameworkCore;
using UZBWalks.Api.Data;
using UZBWalks.Api.Models.Domain;

namespace UZBWalks.Api.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly UzbWalkDbContext _dbContext;

        public RegionRepository(UzbWalkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null) return null;

            _dbContext.Regions.Remove(existingRegion);
            await _dbContext.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null) return null;

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await _dbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
