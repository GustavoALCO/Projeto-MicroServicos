using AdsService.Aplication.Interfaces;
using AdsService.Dommain.Interfaces.Product;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdsService.Aplication.Commands.Product.Handler;

public class CreateproductsHandler : IRequestHandler<CreateProductCommands, Unit>
{
    private readonly IProductRepositoryCommands _commands;

    private readonly IValidator<CreateProductCommands> _validator;

    private readonly ILogger<CreateproductsHandler> _logger;

    private readonly IConvertImagesByte _imagesByte;

    public CreateproductsHandler(IProductRepositoryCommands productRepositoryCommands, ILogger<CreateproductsHandler> logger, IValidator<CreateProductCommands> validator, IConvertImagesByte imagesByte)
    {
        _commands = productRepositoryCommands;
        _logger = logger;
        _validator = validator;
        _imagesByte = imagesByte;
    }

    public Task<Unit> Handle(CreateProductCommands request, CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(request);
        if (!validation.IsValid)
        {   //se nao for valido coleta os erros do Fluent Validation
            var errors = validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            //Detalhando os Erros no Log para uma analise futura 
            _logger.LogWarning("Falha na validação do Anuncio: {Errors}", string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

            throw new ValidationException(validation.Errors);
        }

        // Iniciando lista de bytes para armazenar as imagens convertidas
        List<byte[]> bytes = new List<byte[]>();

        // Gerando um novo Id para o produto
        var ProductIdProduct = Guid.NewGuid();

        // Convertendo as imagens recebidas em bytes
        foreach (string imagebytes in request.Data)
        {
            bytes.Add(_imagesByte.ConvertImageToByteAsync(imagebytes));
        }

        // Criando a lista de imagens associadas ao produto
        var images = request.Data.Select(data => new Dommain.Entities.Images
        {
            IdImage = Guid.NewGuid(),
            ProductIdProduct = ProductIdProduct,
            FileName = request.FileName,
            ContentType = request.ContentType,
            Data = bytes
        }).ToList();

        // Criando o produto com os dados recebidos e as imagens convertidas
        var product = new Dommain.Entities.Product
        {
            IdProduct = ProductIdProduct,
            IdUser = request.IdUser,
            Title = request.Title,
            Description = request.Description,
            State = request.State,
            Images = images,
            City = request.City,
            ZipCode = request.ZipCode,
            IsValid = true, 
            Category = request.Category,
            Boost = request.Boost
        };

        _logger.LogInformation("Criando Produto: {Product}", product);

        // Chamando o repositório para criar o produto no banco de dados
        _commands.CreateAsync(product);

        _logger.LogInformation("Produto criado com sucesso: {ProductId}", product.IdProduct);

        // Retornando sucesso após a criação do produto
        return Task.FromResult(Unit.Value);
    }
}
