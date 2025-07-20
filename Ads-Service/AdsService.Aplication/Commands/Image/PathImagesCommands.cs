using MediatR;

namespace AdsService.Aplication.Commands.Image;

public class PathImagesCommands : IRequest<Unit>
{
    public required Guid ProductIdProduct { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public List<string> Data { get; set; }
}
