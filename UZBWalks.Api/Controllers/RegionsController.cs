using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UZBWalks.Api.Data;
using UZBWalks.Api.Models.Domain;
using UZBWalks.Api.Models.DTO;

namespace UZBWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly UzbWalkDbContext _dbConetxt;
        public RegionsController(UzbWalkDbContext dbContext)
        {
            _dbConetxt = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var regionDomain = _dbConetxt.Regions.ToList();

            var regionsDto = new List<RegionDto>();

            foreach (var region in regionDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var regionDomain = _dbConetxt.Regions.FirstOrDefault(c => c.Id == id);

            if (regionDomain == null) NotFound();

            var regionDto = new RegionDto
            {
                Id = regionDomain!.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegion)
        {
            var regionModel = new Region
            {
                Name = addRegion.Name,
                Code = addRegion.Code,
                RegionImageUrl = addRegion.RegionImageUrl
            };

            _dbConetxt.Regions.Add(regionModel);
            _dbConetxt.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionModel.Id,
                Name = regionModel.Name,
                Code = regionModel.Code,
                RegionImageUrl = regionModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, UpdateRegionRequestDto updateRegion)
        {
            var regionDomain = _dbConetxt.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomain == null) return NotFound();

            regionDomain.Code = updateRegion.Code;
            regionDomain.Name = updateRegion.Name;
            regionDomain.RegionImageUrl = updateRegion.RegionImageUrl;

            _dbConetxt.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            var regionDomain = _dbConetxt.Regions.FirstOrDefault(c => c.Id == id);

            if(regionDomain == null) return NotFound();

            _dbConetxt.Remove(regionDomain);
            _dbConetxt.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
