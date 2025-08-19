using AdsService.Dommain.Entities;
using AdsService.Dommain.Interfaces.Product;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AdsService.Aplication.Query.Handler;

public class GetProductsFilterHandler : IRequestHandler<GetProductsFilterQuery, List<Dommain.Entities.Product>>
{
    private readonly IProductRepositoryQuery _query;

    private readonly ILogger<GetProductsFilterHandler> _logger;

    public GetProductsFilterHandler(IProductRepositoryQuery query, ILogger<GetProductsFilterHandler> logger)
    {
        _query = query;
        _logger = logger;
    }

    public async Task<List<Product>> Handle(GetProductsFilterQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page;

        if (page < 1)
        {
            _logger.LogInformation("Adicionando o valor de 1 para que tenha uma página válida");
            page = 1;
        }

        page = page * 10;

        var queryable = _query.GetQueryable();

        if (!string.IsNullOrEmpty(request.Title))
        {
            // Certifique-se de que 'GetQueryable()' retorna um IQueryable para que o método Where funcione corretamente
            queryable = queryable.Where(x => x.Title.ToLower().Contains(request.Title.ToLower()));
        }
        if (string.IsNullOrEmpty(request.Description))
        {
            queryable = queryable.Where(x => x.Description.ToLower().Contains(request.Description.ToLower()));
        }
        if (!string.IsNullOrEmpty(request.State))
        {
            queryable = queryable.Where(x => x.State.ToLower().Contains(request.State.ToLower()));
        }
        if (!string.IsNullOrEmpty(request.City))
        {
            queryable = queryable.Where(x => x.City.ToLower().Contains(request.City.ToLower()));
        }
        if (request.Category != null)
        {
            queryable = queryable.Where(x => x.Category == request.Category);
        }
        if (request.IsValid)
        {
            queryable = queryable.Where(x => x.IsValid == request.IsValid);
        }

        _logger.LogInformation("Buscando produtos com os filtros informados.");

        var products = await _query.GetProductFilter(queryable);

        if (products.Count == 0)
        {
            _logger.LogInformation("Nenhum produto encontrado com os filtros informados.");
            return new List<Product>();
        }

        _logger.LogInformation($"Foram encontrados {products.Count} produtos com os filtros informados.");
        return products.Skip(page - 10).Take(10).ToList();
    }
    }
