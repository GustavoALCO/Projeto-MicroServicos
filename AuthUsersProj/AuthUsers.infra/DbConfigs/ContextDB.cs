using ChatService.dommain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthUsers.infra.DbConfig;

public class ContextDB : DbContext
{
    public ContextDB(DbContextOptions<ContextDB> options) : base(options)
    {    
     }

    public DbSet<Chat> Chat { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Chat>()
            .OwnsMany(x => x.Mensages);

        //modelBuilder.Entity<Chat>()
        //    .OwnsMany(x => x.User);
    }
   }
