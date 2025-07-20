using MediatR;
using Microsoft.Extensions.Logging;

namespace AdsService.Aplication.Query.Handler;

public class GetAllProductsUserHandler : IRequestHandler<GetAllProductsUserQuery, List<Dommain.Entities.Product>>
{
    private readonly Dommain.Interfaces.Product.IProductRepositoryQuery _productRepositoryQuery;

    private readonly ILogger<GetAllProductsUserHandler> _logger;

    public GetAllProductsUserHandler(Dommain.Interfaces.Product.IProductRepositoryQuery productRepositoryQuery, ILogger<GetAllProductsUserHandler> logger)
    {
        _productRepositoryQuery = productRepositoryQuery;
        _logger = logger;
    }
    public async Task<List<Dommain.Entities.Product>> Handle(GetAllProductsUserQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page;

        if (page < 1)
        {
            _logger.LogInformation("Adicionando o valor de um ja que a pagina esta negativa ou 0");
            page = 1;
        }

        page = page * 10;

        return await _productRepositoryQuery.GetAllProductsUser(request.IdUser, page);
    }
}

