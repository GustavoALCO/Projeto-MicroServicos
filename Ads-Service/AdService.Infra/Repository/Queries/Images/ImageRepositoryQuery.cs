using AdsService.Dommain.Interfaces.Image;
using AuthUsers.infra.DbConfig;
using Microsoft.EntityFrameworkCore;

namespace AdsService.Infra.Repository.Queries.Images;

public class ImageRepositoryQuery : IImageRepositoryQuery
{
    private readonly ContextDB _context;
    public ImageRepositoryQuery(ContextDB context)
    {
        _context = context;
    }
    public async Task<Dommain.Entities.Images> GetImageByIdProductAsync(Guid ProductID)
    {
        var Images = await _context.Images.FirstOrDefaultAsync(i => i.ProductIdProduct == ProductID);

        return Images;      
    }
}
