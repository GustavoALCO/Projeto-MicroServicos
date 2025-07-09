using AdsService.Dommain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthUsers.infra.DbConfig;

public class ContextDB : DbContext
{
    public ContextDB(DbContextOptions<ContextDB> options) : base(options)
    {    
    }

    public DbSet<Product> Products { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Declarando entidade produto
        modelBuilder.Entity<Product>()
         .OwnsOne(p => p.Category, categoryBuilder =>
         {
             // Categoria principal
             categoryBuilder.Property(c => c.CategoryType)
                 .HasConversion<string>();

             // Carro dentro da categoria (se existir)
             categoryBuilder.OwnsOne(c => c.Car, carBuilder =>
             {
                 carBuilder.Property(c => c.CarBrand)
                     .HasConversion<string>();

                 carBuilder.Property(c => c.Fuel)
                     .HasConversion<string>();

                 carBuilder.Property(c => c.Color)
                     .HasConversion<string>();
             });

             // Imóvel dentro da categoria (se existir)
             categoryBuilder.OwnsOne(c => c.House, houseBuilder =>
             {
                 houseBuilder.Property(h => h.House) 
                     .HasConversion<string>();
             });
         });

        modelBuilder.Entity<Product>()
            .OwnsMany(p => p.Images);
    }
   }
