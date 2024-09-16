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

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = _dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();

            // Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
            }

            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    walks = isAscending ? walks.OrderBy(x => x.Name): walks.OrderByDescending(x => x.Name);
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
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
