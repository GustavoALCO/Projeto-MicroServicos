using AuthUsers.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthUsers.infra.DbConfig;

public class ContextDB : DbContext
{
    public ContextDB(DbContextOptions<ContextDB> options) : base(options)
    {    
     }

    public DbSet<Employee> Employee { get; set; }

    public DbSet<Users> Users { get; set; }

    public DbSet<Adress> Adress { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<AuditLog>();

        // Configurando Que Um Usuario Pode ter Varios Endereços
        modelBuilder.Entity<Users>()
            .HasMany(x => x.Adress)
            .WithOne(x => x.Users)
            .HasForeignKey(x => x.IdUser);
            
    }
   }
