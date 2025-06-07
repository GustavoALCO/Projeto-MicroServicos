using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace AuthUsers.infra.DbConfig;

public class ContextDBFactory : IDesignTimeDbContextFactory<DbConfig>
{
    public DbConfig CreateDbContext(string[] args)
    {

        var optionsBuilder = new DbContextOptionsBuilder<DbConfig>();


        optionsBuilder.UseNpgsql("Server=localhost,8002;Database=WEBAPI;User Id=sa;Password=P@ssw0rd!;TrustServerCertificate=True;");

        return new DbConfig(optionsBuilder.Options);
    }
}

