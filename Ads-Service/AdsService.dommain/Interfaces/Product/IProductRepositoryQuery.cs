namespace AdsService.Dommain.Interfaces.Product;

public interface IProductRepositoryQuery
{
    Task<Dommain.Entities.Product?> GetByIdAsync(Guid idProduct);

    Task<List<Entities.Product>> GetAllAsync(int page);

    Task<List<Entities.Product>> GetAllProductsUser(Guid idUser, int page);

    Task<List<Entities.Product>> GetProductFilter(IQueryable queryable);

    Task<IQueryable> GetQueryable();
}
