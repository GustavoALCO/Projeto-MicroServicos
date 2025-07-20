using MediatR;

namespace AdsService.Aplication.Query;

public class GetAllProductsUserQuery : IRequest<List<Dommain.Entities.Product>>
{
    public Guid IdUser { get; set; }
    public int Page { get; set; }

}
