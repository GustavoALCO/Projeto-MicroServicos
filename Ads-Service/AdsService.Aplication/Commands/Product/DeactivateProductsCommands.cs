using MediatR;

namespace AdsService.Aplication.Commands.Product;

public class DeactivateProductsCommands : IRequest<Unit>
{
    public Guid Id { get; set; }
}
