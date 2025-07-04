using AuthUsers.domain.Entities;
using AuthUsers.domain.Interfaces.Adress;
using AuthUsers.infra.DbConfig;

namespace AuthUsers.infra.Repositories.Adress;

public class AdressRepositoryCommands : IAdressRepositoryCommands
{
    private readonly ContextDB _db;

    public AdressRepositoryCommands(ContextDB db)
    {
        _db = db;
    }

    public async Task AddAsync(domain.Entities.Adress adress)
    {
        await _db.Adress.AddAsync(adress);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(domain.Entities.Adress adress)
    {
        _db.Adress.Remove(adress);

        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(domain.Entities.Adress adress)
    {
        _db.Adress.Update(adress);

        await _db.SaveChangesAsync();
    }
}
