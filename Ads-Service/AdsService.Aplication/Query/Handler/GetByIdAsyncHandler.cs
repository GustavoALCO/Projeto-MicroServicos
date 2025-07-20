using AdsService.Dommain.Entities;
using AdsService.Dommain.Interfaces.Product;
using MediatR;

namespace AdsService.Aplication.Query.Handler;

public class GetByIdAsyncHandler : IRequestHandler<GetByIdAsyncQuery, Product>
{
    private readonly IProductRepositoryQuery _productRepositoryQuery;

    public GetByIdAsyncHandler(IProductRepositoryQuery productRepositoryQuery)
    {
        _productRepositoryQuery = productRepositoryQuery;
    }

    public async Task<Product> Handle(GetByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepositoryQuery.GetByIdAsync(request.Id);

        if (product == null)
        {
            throw new Exception("Product not found");
        }

        return product;
    }
}