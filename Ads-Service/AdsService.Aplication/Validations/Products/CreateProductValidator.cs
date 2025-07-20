using AdsService.Aplication.Commands.Product;
using AdsService.Aplication.Interfaces;
using FluentValidation;

namespace AdsService.Aplication.Validations.Products;

public class CreateProductValidator : AbstractValidator<CreateProductCommands>
{
    public CreateProductValidator(IValidateBase64 validateBase64)
    {
        RuleFor(x => x.IdUser)
            .NotEmpty().WithMessage("É Obrigatorio passar o Id do usuario que esta criando o anuncio");

        RuleFor(x => x.Title)
            .NotNull().WithMessage("É Obrigatorio passar um titulo para o seu anuncio")
            .MaximumLength(50).WithMessage("O titulo do anuncio deve ter no maximo 50 caracteres")
            .MinimumLength(5).WithMessage("O titulo do anuncio deve ter no minimo 5 caracteres");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("É Obrigatorio passar uma descrição para o seu anuncio")
            .MaximumLength(500).WithMessage("A descrição do anuncio deve ter no maximo 500 caracteres")
            .MinimumLength(10).WithMessage("A descrição do anuncio deve ter no minimo 10 caracteres");

        RuleFor(x => x.State)
            .NotNull().WithMessage("É Obrigatorio passar um estado para o seu anuncio")
            .Length(2).WithMessage("O estado do anuncio deve ter 2 caracteres");

        RuleFor(x => x.City)
            .NotNull().WithMessage("É Obrigatorio passar uma cidade para o seu anuncio")
            .MinimumLength(3).WithMessage("A cidade do anuncio deve ter no minimo 3 caracteres");

        RuleFor(x => x.Category.CategoryType)
            .NotNull().WithMessage("É Obrigatorio passar o tipo de categoria do anuncio")
            .IsInEnum().WithMessage("O tipo de categoria do anuncio deve ser um valor valido");

        RuleFor(x => x.Data)
            .NotEmpty().WithMessage("É Obrigatorio passar pelo menos uma imagem para o seu anuncio")
            .Must(img => img.Count > 0).WithMessage("É Obrigatorio passar pelo menos uma imagem para o seu anuncio")
            .Must(img => img.Count <= 9).WithMessage("O anuncio pode ter no maximo 9 imagens")
            .Must(img => validateBase64.IsValidBase64String(img)).WithMessage("A imagem em Base64 não é válida.");
    }
}
