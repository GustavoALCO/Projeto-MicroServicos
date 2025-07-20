namespace AdsService.Dommain.Interfaces.Image;

public interface IImageRepositoryQuery
{
    Task<Dommain.Entities.Images> GetImageByIdProductAsync(Guid ProductID);
}
