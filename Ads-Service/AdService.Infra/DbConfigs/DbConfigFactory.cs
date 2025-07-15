using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AuthUsers.infra.DbConfig;

public class ContextDBFactory : IDesignTimeDbContextFactory<ContextDB>
{
    public ContextDB CreateDbContext(string[] args)
    {

        var optionsBuilder = new DbContextOptionsBuilder<ContextDB>();

        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=adsdb;Username=adsuser;Password=Teste123");

        return new ContextDB(optionsBuilder.Options);
    }
}

