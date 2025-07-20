using AdsService.Dommain.Interfaces.Product;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdsService.Aplication.Commands.Product.Handler;

public class DeleteProductsHandler : IRequestHandler<DeleteProductsCommands, Unit>
{
    private readonly IProductRepositoryCommands _commands;

    private readonly ILogger<DeleteProductsHandler> _logger;

    private readonly IProductRepositoryQuery _query;

    public DeleteProductsHandler(ILogger<DeleteProductsHandler> logger, IProductRepositoryCommands commands, IProductRepositoryQuery query)
    {
        _logger = logger;
        _commands = commands;
        _query = query;
    }

    public async Task<Unit> Handle(DeleteProductsCommands request, CancellationToken cancellationToken)
    {
        var products = await _query.GetByIdAsync(request.IdProduct);
            
        if (products == null)
        {
            _logger.LogWarning("Produto com Id {IdProduct} não encontrado.", request.IdProduct);
            throw new KeyNotFoundException($"Produto com Id {request.IdProduct} não encontrado.");
        }

        _logger.LogInformation("Excluindo Produto: {ProductId}", request.IdProduct);

        await _commands.DeleteAsync(products);

        return Unit.Value;
    }
}
