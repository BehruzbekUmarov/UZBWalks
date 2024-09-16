using AutoMapper;
using UZBWalks.Api.Models.Domain;
using UZBWalks.Api.Models.DTO;

namespace UZBWalks.Api.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();

            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
