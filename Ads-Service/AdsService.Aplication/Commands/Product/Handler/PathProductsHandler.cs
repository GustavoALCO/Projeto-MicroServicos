using AdsService.Dommain.Entities;
using AdsService.Dommain.Interfaces.Product;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdsService.Aplication.Commands.Product.Handler;

public class PathProductsHandler :IRequestHandler<PathProductsCommands, Unit>
{
    private readonly IProductRepositoryCommands _commands;

    private readonly ILogger<PathProductsHandler> _logger;

    private readonly IProductRepositoryQuery _query;

    public PathProductsHandler(ILogger<PathProductsHandler> logger, IProductRepositoryCommands commands, IProductRepositoryQuery query)
    {
        _logger = logger;
        _commands = commands;
        _query = query;
    }

    public async Task<Unit> Handle(PathProductsCommands request, CancellationToken cancellationToken)
    {
        var product = await _query.GetByIdAsync(request.IdProduct);

        if (product == null)
        {
            _logger.LogWarning("Produto com Id {IdProduct} não encontrado.", request.IdProduct);
            throw new KeyNotFoundException($"Produto com Id {request.IdProduct} não encontrado.");
        }

        // Atualizando os campos do produto com os dados recebidos
        product.Title = request.Title ?? product.Title;
        product.Description = request.Description ?? product.Description;
        product.Category = request.Category ?? product.Category;
        product.IsValid = request.isValid;

        _logger.LogInformation("Atualizando Produto: {Product}", product);

        await _commands.UpdateAsync(product);

        _logger.LogInformation("Produto atualizado com sucesso: {IdProduct}", request.IdProduct);

        return Unit.Value;
    }
}