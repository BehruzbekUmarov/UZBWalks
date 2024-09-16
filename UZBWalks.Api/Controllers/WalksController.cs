using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UZBWalks.Api.CustomActionFilters;
using UZBWalks.Api.Models.Domain;
using UZBWalks.Api.Models.DTO;
using UZBWalks.Api.Repositories;

namespace UZBWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        // Get Walks
        // Get: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAcending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walkDomain = await _walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending?? true,
                pageNumber,pageSize);
            if (walkDomain == null) return NotFound();

            return Ok(_mapper.Map<List<WalkDto>>(walkDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepository.GetByIdAsync(id);
            if (walkDomain == null) return NotFound();

            return Ok(_mapper.Map<WalkDto>(walkDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto requestDto)
        {
            var walkDomain = _mapper.Map<Walk>(requestDto);
            
            walkDomain = await _walkRepository.CreateAsync(walkDomain);
            if (walkDomain == null) return NotFound();

            var walkDto = _mapper.Map<WalkDto>(walkDomain);

            return CreatedAtAction(nameof(GetById), new {id =  walkDto.Id}, walkDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] UpdateWalkRequestDto requestDto)
        {
            var walkDomain = _mapper.Map<Walk>(requestDto);
            
            walkDomain = await _walkRepository.UpdateAsync(id, walkDomain);
            if (walkDomain == null) return NotFound();

            return Ok(_mapper.Map<WalkDto>(walkDomain));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepository.DeleteAsync(id);
            if (walkDomain == null) return NotFound();

            return Ok(_mapper.Map<WalkDto>(walkDomain));
        }
    }
}
