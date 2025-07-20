


namespace AdsService.Dommain.Interfaces.Product;

public interface IProductRepositoryCommands
{
    Task DeleteAsync(Entities.Product product);

    Task UpdateAsync(Entities.Product product);

    Task CreateAsync(Entities.Product product);
    
}
