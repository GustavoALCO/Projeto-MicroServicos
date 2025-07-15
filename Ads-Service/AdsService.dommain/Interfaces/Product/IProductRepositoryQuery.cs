namespace AdsService.Dommain.Interfaces.Product;

public interface IProductRepositoryQuery
{
    Task<Dommain.Entities.Product?> GetByIdAsync(Guid idProduct);

    Task<List<Dommain.Entities.Product>> GetAllAsync(int page);
}
