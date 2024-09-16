using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UZBWalks.Api.CustomActionFilters;
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
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        public RegionsController(IMapper mapper, IRegionRepository regionRepository)
        {
            _mapper = mapper;
            _regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionDomain = await _regionRepository.GetAllAsync();

            if(regionDomain == null) return NotFound();

            return Ok(_mapper.Map<List<RegionDto>>(regionDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.GetByIdAsync(id);

            if (regionDomain == null) NotFound();

            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegion)
        {
            var regionModel = _mapper.Map<Region>(addRegion);

            regionModel = await _regionRepository.CreateAsync(regionModel);

            var regionDto = _mapper.Map<RegionDto>(regionModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateRegionRequestDto updateRegion)
        {
            var regionDomianModel = _mapper.Map<Region>(updateRegion);

            regionDomianModel = await _regionRepository.UpdateAsync(id, regionDomianModel);

            if(regionDomianModel == null) return NotFound();

            var regionDto = _mapper.Map<RegionDto>(regionDomianModel);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var regionDomain = await _regionRepository.DeleteAsync(id);

            if (regionDomain == null) return NotFound();

            var regionDto = _mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }
    }
}
