using AdsService.Dommain.Interfaces.Product;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdsService.Aplication.Commands.Product.Handler;

public class DeactivateProductsHandler : IRequestHandler<DeactivateProductsCommands, Unit>
{
    private readonly IProductRepositoryCommands _Commands;

    private readonly IProductRepositoryQuery _Queries;

    private readonly ILogger<DeactivateProductsHandler> _logger;

    public DeactivateProductsHandler(ILogger<DeactivateProductsHandler> logger, IProductRepositoryQuery queries, IProductRepositoryCommands commands)
    {
        _logger = logger;
        _Queries = queries;
        _Commands = commands;
    }

    public async Task<Unit> Handle(DeactivateProductsCommands request, CancellationToken cancellationToken)
    {
        
        var product = await _Queries.GetByIdAsync(request.Id);

        if (product == null)
        {
            _logger.LogError($"Product with ID {request.Id} not found.");
            throw new Exception($"Product with ID {request.Id} not found.");
        }

        _logger.LogInformation("Produto encontrado para desativação: {ProductId}", request.Id);

        product.IsValid = false;

        await _Commands.UpdateAsync(product);

        return Unit.Value;
    }
}