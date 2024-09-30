using FluentValidation;
using UZBWalks.Api.Models.DTO;

namespace UZBWalks.Api.Validators
{
    public class UpdateRegionRequestDtoValidator : AbstractValidator<UpdateRegionRequestDto>
    {
        public UpdateRegionRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Code).MaximumLength(3).MinimumLength(3);
        }
    }
}
