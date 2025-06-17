using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace AuthUsers.infra.DbConfig;

public class ContextDBFactory : IDesignTimeDbContextFactory<DbConfig>
{
    public DbConfig CreateDbContext(string[] args)
    {

        var optionsBuilder = new DbContextOptionsBuilder<DbConfig>();


        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=authdb;Username=authuser;Password=Teste123");

        return new DbConfig(optionsBuilder.Options);
    }
}

