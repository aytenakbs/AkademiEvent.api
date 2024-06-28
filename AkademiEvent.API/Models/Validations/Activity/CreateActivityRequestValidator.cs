using AkademiEvent.API.Models.DTO.activity;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace AkademiEvent.API.Models.Validations.Activity;

public class CreateActivityRequestValidator:AbstractValidator<CreateActivityRequestDto>
{
    public CreateActivityRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Etkinlik adı boş geçilemez");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Etkinlik açıklama kısmı boş geçilemez");
                      
    }
}
