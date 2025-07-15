using AdsService.Dommain.Interfaces.Images;
using AuthUsers.infra.DbConfig;
using Microsoft.Extensions.Logging;

namespace AdsService.Infra.Repository.Commands.Images;

public class ImageRepositoryCommands : IImageRepositoryCommands
{
    private readonly ContextDB _context;
    private readonly ILogger<ImageRepositoryCommands> _logger;
    public ImageRepositoryCommands(ContextDB context, ILogger<ImageRepositoryCommands> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task CreateAsync(Dommain.Entities.Images image)
    {
        await _context.Images.AddAsync(image);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Image with ID {image.IdImage} created successfully.");
    }

    public async Task UpdateAsync(Dommain.Entities.Images image)
    {
        _context.Images.Update(image);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Image with ID {image.IdImage} updated successfully.");
    }

    public async Task DeleteAsync(Dommain.Entities.Images image)
    {
        _context.Images.Remove(image);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Image with ID {image.IdImage} deleted successfully.");
    }
}
