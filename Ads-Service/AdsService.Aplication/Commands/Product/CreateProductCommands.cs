using AdsService.Dommain.Entities;
using MediatR;

namespace AdsService.Aplication.Commands.Product;

public class CreateProductCommands : IRequest<Unit>
{
    public required Guid IdUser { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string State { get; set; }

    public required string City { get; set; }

    public required string ZipCode { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public List<string> Data { get; set; }

    public Category Category { get; set; }
}
