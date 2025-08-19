using AdsService.Dommain.Entities;
using MediatR;

namespace AdsService.Aplication.Query;

public class GetProductsFilterQuery : IRequest<List<Product>>
{

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string State { get; set; }

    public required string City { get; set; }

    public required bool IsValid { get; set; }

    public Category Category { get; set; }

    public int Page { get; set; }
}

