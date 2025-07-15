namespace AdsService.Dommain.Interfaces.Image;

public interface IImageRepositoryQuery
{
    Task<List<Dommain.Entities.Images>> GetImageByIdProductAsync(Guid ProductID);
}
