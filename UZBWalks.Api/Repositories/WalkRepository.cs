using Microsoft.EntityFrameworkCore;
using UZBWalks.Api.Data;
using UZBWalks.Api.Models.Domain;

namespace UZBWalks.Api.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly UzbWalkDbContext _dbContext;

        public WalkRepository(UzbWalkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingDomain = _dbContext.Walks.FirstOrDefault(x => x.Id == id);
            if (existingDomain == null) return null;

            _dbContext.Walks.Remove(existingDomain);
            await _dbContext.SaveChangesAsync();

            return existingDomain;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _dbContext.Walks
                .Include(x => x.Difficulty)
                .Include(x => x.Region)
                .ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Walks
                .Include(x => x.Difficulty)
                .Include(x => x.Region)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingDomain = await _dbContext.Walks.FirstOrDefaultAsync(y => y.Id == id);
            if (existingDomain == null) return null;

            existingDomain.Description = walk.Description;
            existingDomain.Name = walk.Name;
            existingDomain.DifficultyId = walk.DifficultyId;
            existingDomain.LengthInKm = walk.LengthInKm;
            existingDomain.WalkImageUrl = walk.WalkImageUrl;
            existingDomain.RegionId = walk.RegionId;

            await _dbContext.SaveChangesAsync();

            return existingDomain;
        }
    }
}
