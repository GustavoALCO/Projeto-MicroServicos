using AdsService.Dommain.Interfaces.Product;
using AuthUsers.infra.DbConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AdsService.Infra.Repository.Queries.Product;

public class ProductRepositoryQuery : IProductRepositoryQuery
{
    private readonly ContextDB _context;
    private readonly ILogger<ProductRepositoryQuery> _logger;

    public ProductRepositoryQuery(ContextDB context, ILogger<ProductRepositoryQuery> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Dommain.Entities.Product?> GetByIdAsync(Guid idProduct)
    {
        var product = await _context.Products.FindAsync(idProduct);
        _logger.LogInformation($"busca do ID {idProduct} com sucesso.");
        return product;
    }
    public async Task<List<Dommain.Entities.Product>> GetAllAsync(int page)
    {
        return await _context.Products.Take(page).ToListAsync();
    }
}
