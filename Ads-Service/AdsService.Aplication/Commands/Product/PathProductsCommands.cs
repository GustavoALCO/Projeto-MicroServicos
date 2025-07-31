using System.ComponentModel.DataAnnotations;
using AdsService.Dommain.Entities;
using MediatR;

namespace AdsService.Aplication.Commands.Product;

public class PathProductsCommands : IRequest<Unit>
{
    public required Guid IdProduct { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public bool isValid { get; set; }   

    public bool Boost { get; set; }

    public Category Category { get; set; }
}
