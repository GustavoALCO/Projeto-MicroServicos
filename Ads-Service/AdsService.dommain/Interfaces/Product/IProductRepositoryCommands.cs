namespace AdsService.Dommain.Interfaces.Product;

public interface IProductRepositoryCommands
{
    Task DeleteAsync(Dommain.Entities.Product product);

    Task UpdateAsync(Dommain.Entities.Product product);

    Task CreateAsync(Dommain.Entities.Product product);
}
