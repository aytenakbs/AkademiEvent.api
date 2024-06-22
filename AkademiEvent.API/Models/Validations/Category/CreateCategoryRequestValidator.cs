using AkademiEvent.API.Models.DTO.category;
using FluentValidation;

namespace AkademiEvent.API.Models.Validations.Category;

public class CreateCategoryRequestValidator:AbstractValidator<CreateCategoryRequestDto>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name alanı boş geçilemez")
            .MinimumLength(2).WithMessage("Girilen değer 2 karakterden kısa olamaz"); 
        
    }
}

