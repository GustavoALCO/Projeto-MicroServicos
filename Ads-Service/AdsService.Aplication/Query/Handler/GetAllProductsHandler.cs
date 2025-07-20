using AdsService.Dommain.Entities;
using AdsService.Dommain.Interfaces.Product;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdsService.Aplication.Query.Handler;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Dommain.Entities.Product>>
{
    private readonly ILogger<GetAllProductsHandler> _logger;

    private readonly IProductRepositoryQuery _query;

    public GetAllProductsHandler(IProductRepositoryQuery query, ILogger<GetAllProductsHandler> logger)
    {
        _query = query;
        _logger = logger;
    }

    public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page;

        if ( page < 1)
        {
            _logger.LogInformation("Adicionando o valor de um ja que a pagina esta negativa ou 0");
            page = 1;
        }

        page = page * 10;

        _logger.LogInformation("Definiindo o valor de busca de produtos em {Page}", page);

        var products = await _query.GetAllAsync(page);

        _logger.LogInformation("Busca de produtos realizada com sucesso");

        return products;
    }
}
