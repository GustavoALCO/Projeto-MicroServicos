using MediatR;

namespace AdsService.Aplication.Commands.Product;

public class ActivateProductsCommands : IRequest<Unit>
{
    public Guid Id { get; set; }
}
