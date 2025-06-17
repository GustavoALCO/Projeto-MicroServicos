using AuthUsers.domain.Interfaces.Users;
using AuthUsers.infra.DbConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthUsers.infra.Repositories.Users;

public class UsersRepositoryCommands : IUserRepositoryCommands
{
    private readonly DbConfig.DbConfig _dbConfig;

    private readonly DbSet<domain.Entities.Users> _DBUsers;

    private readonly ILogger<UsersRepositoryCommands> _logger;

    public UsersRepositoryCommands(DbConfig.DbConfig dbConfig,ILogger<UsersRepositoryCommands> logger)
    {
        _dbConfig = dbConfig;
        _DBUsers = _dbConfig.Users;
        _logger = logger;
    }


    public async Task CreateUserAsync(domain.Entities.Users user)
    {
        await _DBUsers.AddAsync(user);

        _logger.LogInformation($"Usuario do email : {user.Email} adicionado ao banco de dados.");

        await _dbConfig.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(domain.Entities.Users user)
    {
        _dbConfig.Remove(user);

        _logger.LogInformation($"Usuario do email : {user.Email} removido do banco de dados.");

        await _dbConfig.SaveChangesAsync();
    }

    public async Task UpdateUserAync(domain.Entities.Users user)
    {
        _DBUsers.Update(user);

        _logger.LogInformation($"Usuario do email : {user.Email} foi atualizado no banco de dados.");

        await _dbConfig.SaveChangesAsync();
    }
}
