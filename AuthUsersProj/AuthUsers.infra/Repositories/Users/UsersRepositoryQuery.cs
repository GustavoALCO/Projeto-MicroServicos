using System.Linq;
using AuthUsers.domain.Interfaces.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace AuthUsers.infra.Repositories.Users;

public class UsersRepositoryQuery : IUserRepositoryQuery
{

    private readonly DbConfig.DbConfig _db;

    private readonly DbSet<domain.Entities.Users> _dbusers;

    private readonly ILogger<UsersRepositoryQuery> _logger;

    public UsersRepositoryQuery(DbConfig.DbConfig db, ILogger<UsersRepositoryQuery> logger)
    {
        _db = db;
        _dbusers = _db.Users;
        _logger = logger;
    }

    public async Task<IEnumerable<domain.Entities.Users?>> GetAllUsersAsync(int page)
    {
        var Users = await _dbusers.Where(x => x.IsValid == true).Take(page).ToListAsync();

        if (Users.Count() < 1)
            _logger.LogWarning($"Não existe nenhum Usuario para ser Mostrado");


        return Users;
    }

    public async Task<domain.Entities.Users> GetUserCPFAsync(string cpf)
    {
        var users = await _dbusers.FirstOrDefaultAsync(x => x.Cpf == cpf);

        if (users == null)
        {
            _logger.LogWarning($"Usuário com cpf '{cpf}' não encontrado.");
        }

        return users;
    }

    public async Task<domain.Entities.Users?> GetUserEmailAsync(string email)
    {
        var users = await _dbusers.FirstOrDefaultAsync(x => x.Email == email);

        if (users == null)
        {
            _logger.LogWarning($"Usuário com e-mail '{email}' não encontrado.");
        }

        return users;
    }

    public async Task<domain.Entities.Users?> GetUserIdAsync(Guid Id)
    {
        var users = await _dbusers.FirstOrDefaultAsync(x => x.IdUser == Id);

        if (users == null)
        {
            _logger.LogWarning($"Usuario com o Id : '{Id}' não encontrado.");
        }

        return users;
    }

    public async Task<IEnumerable<domain.Entities.Users>?> UserFilterAsync(IQueryable<domain.Entities.Users> users, int page)
    {
        var Users = await users.Take(page).ToListAsync();

        if (users == null)
        {
            _logger.LogWarning($"Usuarios não encontrado pelo filtro");
            return null;
        }

        return Users;
    }

    public IQueryable<domain.Entities.Users> UserQuerable()
    {
        return _dbusers.AsQueryable();
    }
}
