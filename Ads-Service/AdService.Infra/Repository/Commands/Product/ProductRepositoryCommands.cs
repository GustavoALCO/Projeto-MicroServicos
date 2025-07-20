using AdsService.Dommain.Entities;
using AdsService.Dommain.Interfaces.Product;
using AuthUsers.infra.DbConfig;
using Microsoft.Extensions.Logging;

namespace AdsService.Infra.Repository.Commands.Product;

public class ProductRepositoryCommands : IProductRepositoryCommands
{
    private readonly ContextDB _context;

    private readonly ILogger<ProductRepositoryCommands> _logger;

    public ProductRepositoryCommands(ContextDB context, ILogger<ProductRepositoryCommands> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task CreateAsync(Dommain.Entities.Product product)
    {
        await _context.Products.AddAsync(product);

        await _context.SaveChangesAsync();

        _logger.LogInformation($"Product criados com sucesso.");
    }
    public async Task UpdateAsync(Dommain.Entities.Product product)
    {
        _context.Products.Update(product);

        await _context.SaveChangesAsync();
        
        _logger.LogInformation($"Product com ID {product.IdProduct} alterado com Sucesso.");
    }
    public async Task DeleteAsync(Dommain.Entities.Product product)
    {
        
        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        _logger.LogInformation($"Product com ID {product.IdProduct} Excluido com Sucesso.");
    }

}
