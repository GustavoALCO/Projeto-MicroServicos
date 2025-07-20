using AdsService.Aplication.Commands.Image;
using AdsService.Aplication.Interfaces;
using AdsService.Aplication.Services;
using FluentValidation;

namespace AdsService.Aplication.Validations.Image;

public class PathImagesValidator : AbstractValidator<PathImagesCommands>
{
    public PathImagesValidator(IValidateBase64 validateBase64)
    {
        RuleFor(x => x.Data)
            .NotEmpty().WithMessage("É Obrigatorio passar pelo menos uma imagem para o seu anuncio")
            .Must(img => img.Count > 0).WithMessage("É Obrigatorio passar pelo menos uma imagem para o seu anuncio")
            .Must(img => img.Count <= 9).WithMessage("O anuncio pode ter no maximo 9 imagens")
            .Must(img => validateBase64.IsValidBase64String(img)).WithMessage("A imagem em Base64 não é válida.");
    }
}
