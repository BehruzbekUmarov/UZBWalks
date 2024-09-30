using FluentValidation;
using UZBWalks.Api.Models.DTO;

namespace UZBWalks.Api.Validators
{
    public class AddWalkRequestDtoValidator : AbstractValidator<AddWalkRequestDto>
    {
        public AddWalkRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.LengthInKm).NotEmpty().LessThan(10000);
            RuleFor(x => x.DifficultyId).NotEmpty();
            RuleFor(x => x.RegionId).NotEmpty();
        }
    }
}
