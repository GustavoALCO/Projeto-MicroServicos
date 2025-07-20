using AdsService.Dommain.Entities;
using MediatR;

namespace AdsService.Aplication.Query;

public class GetByIdAsyncQuery : IRequest<Product>
{
    public Guid Id { get; set; }
}
