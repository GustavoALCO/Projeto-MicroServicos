using MediatR;

namespace AdsService.Aplication.Commands.Product;

public class DeleteProductsCommands : IRequest<Unit>
{
    public required Guid IdProduct { get; set; }
}
