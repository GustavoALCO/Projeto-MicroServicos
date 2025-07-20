using MediatR;

namespace AdsService.Aplication.Query;

public class GetAllProductsQuery : IRequest<List<Dommain.Entities.Product>>
{
    public required int Page { get; set; }

}
