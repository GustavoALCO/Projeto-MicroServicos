using AuthUsers.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthUsers.infra.DbConfig;

public class DbConfig : DbContext
{
    public DbConfig(DbContextOptions<DbConfig> options) : base(options)
    {    
     }

    public DbSet<Employee> Employee { get; set; }

    public DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<AuditLog>();
        modelBuilder.Ignore<Adress>();
    }
   }
