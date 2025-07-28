using AdsService.Dommain.Interfaces.Product;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdsService.Aplication.Commands.Product.Handler;

public class ActivateProductsHandler : IRequestHandler<ActivateProductsCommands, Unit>
{
    private readonly IProductRepositoryCommands _commands;

    private readonly IProductRepositoryQuery _query;

    private readonly ILogger<ActivateProductsHandler> _logger;  

    public ActivateProductsHandler(IProductRepositoryCommands commands)
    {
        _commands = commands;
    }

    public async Task<Unit> Handle(ActivateProductsCommands request, CancellationToken cancellationToken)
    {
        var product = await _query.GetByIdAsync(request.Id);

        if ( product == null)
        {
            _logger.LogError($"Nao foi encontrado anuncio com o Id: {request.Id}");
            throw new Exception($"Nao foi encontrado anuncio com o Id: {request.Id}");
        }

        _logger.LogInformation($"Ativando anuncio com o Id: {request.Id}");

        product.IsValid = true;

        await _commands.UpdateAsync(product);

        return Unit.Value;
    }
}