using AdsService.Aplication.Commands.Product.Handler;
using AdsService.Aplication.Commands.Product;
using AdsService.Aplication.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using AdsService.Aplication.Validations.Image;
using AdsService.Dommain.Interfaces.Image;
using AdsService.Dommain.Interfaces.Images;

namespace AdsService.Aplication.Commands.Image.Handler;

public class PathImagesHandler : IRequestHandler<PathImagesCommands, Unit>
{
    private readonly IValidator<PathImagesCommands> _validator;

    private readonly ILogger<PathImagesHandler> _logger;

    private readonly IConvertImagesByte _imagesByte;

    private readonly IImageRepositoryQuery _imageRepositoryQuery;

    private readonly IImageRepositoryCommands _imageRepositoryCommands;

    public PathImagesHandler(IConvertImagesByte imagesByte, ILogger<PathImagesHandler> logger, IValidator<PathImagesCommands> validator, IImageRepositoryCommands imageRepositoryCommands, IImageRepositoryQuery imageRepositoryQuery)
    {
        _imagesByte = imagesByte;
        _logger = logger;
        _validator = validator;
        _imageRepositoryCommands = imageRepositoryCommands;
        _imageRepositoryQuery = imageRepositoryQuery;
    }

    public async Task<Unit> Handle(PathImagesCommands request, CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(request);
        if (!validation.IsValid)
        {   //se nao for valido coleta os erros do Fluent Validation
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Anuncio: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            throw new ValidationException(validation.Errors);
        }

        var images = await _imageRepositoryQuery.GetImageByIdProductAsync(request.ProductIdProduct);

        // Iniciando lista de bytes para armazenar as imagens convertidas
        List<byte[]> bytes = new List<byte[]>();

        // Gerando um novo Id para o produto
        var ProductIdProduct = Guid.NewGuid();

        // Convertendo as imagens recebidas em bytes
        foreach (string imagebytes in request.Data)
        {
            bytes.Add(_imagesByte.ConvertImageToByteAsync(imagebytes));
        }

        // Atualizando as informações da imagem
        images.ContentType = request.ContentType;
        images.FileName = request.FileName;
        images.Data = bytes;

        // Atualizando a imagem no repositório
        await _imageRepositoryCommands.UpdateAsync(images);

        return Unit.Value;
    }
}
