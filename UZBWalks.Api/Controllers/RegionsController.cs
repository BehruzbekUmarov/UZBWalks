using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UZBWalks.Api.Data;
using UZBWalks.Api.Models.Domain;
using UZBWalks.Api.Models.DTO;
using UZBWalks.Api.Repositories;

namespace UZBWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly UzbWalkDbContext _dbConetxt;
        private readonly IRegionRepository _regionRepository;
        public RegionsController(UzbWalkDbContext dbContext, IRegionRepository regionRepository)
        {
            _dbConetxt = dbContext;
            _regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionDomain = await _regionRepository.GetAllAsync();

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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.GetByIdAsync(id);

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
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegion)
        {
            var regionModel = new Region
            {
                Name = addRegion.Name,
                Code = addRegion.Code,
                RegionImageUrl = addRegion.RegionImageUrl
            };

            regionModel = await _regionRepository.CreateAsync(regionModel);

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
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateRegionRequestDto updateRegion)
        {
            var regionDomianModel = new Region
            {
                Code = updateRegion.Code,
                RegionImageUrl = updateRegion.RegionImageUrl,
                Name = updateRegion.Name,
            };

            regionDomianModel = await _regionRepository.UpdateAsync(id, regionDomianModel);

            if(regionDomianModel == null) return NotFound();

            var regionDto = new RegionDto
            {
                Id = regionDomianModel.Id,
                Name = regionDomianModel.Name,
                Code = regionDomianModel.Code,
                RegionImageUrl = regionDomianModel.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var regionDomain = await _regionRepository.DeleteAsync(id);

            if (regionDomain == null) return NotFound();

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
