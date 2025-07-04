using AuthUsers.domain.Interfaces.Adress;
using AuthUsers.infra.DbConfig;
using Microsoft.EntityFrameworkCore;

namespace AuthUsers.infra.Repositories.Adress;

public class AdressRepositoryQuery : IAdressRepositoryQuery
{

    private readonly ContextDB _db;

    public AdressRepositoryQuery(ContextDB db)
    {
        _db = db;
    }

    public async Task<IEnumerable<domain.Entities.Adress>> GetAllAsync(Guid idUser,int page)
    {

        var adress = await _db.Adress.Where(x => x.IdUser == idUser).Take(page).ToListAsync();

        return adress;
    }

    public async Task<domain.Entities.Adress?> GetByIdAsync(int idAdress)
    {
        var adress = await _db.Adress.FirstOrDefaultAsync(x => x.Id == idAdress);

        return adress;
    }
}
