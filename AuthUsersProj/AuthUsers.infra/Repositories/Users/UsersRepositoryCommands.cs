using AuthUsers.domain.Interfaces.Users;
using AuthUsers.infra.DbConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthUsers.infra.Repositories.Users;

public class UsersRepositoryCommands : IUserRepositoryCommands
{
    private readonly DbConfig.DbConfig _dbConfig;

    private readonly DbSet<domain.Entities.Users> _DBUsers;

    private readonly IUserRepositoryQuery _query;

    private readonly ILogger _logger;

    public UsersRepositoryCommands(DbConfig.DbConfig dbConfig, DbSet<domain.Entities.Users> users, IUserRepositoryQuery query, ILogger logger)
    {
        _dbConfig = dbConfig;
        _DBUsers = _dbConfig.Users;
        _query = query;
        _logger = logger;
    }

    public async Task ActivateUserAsync(Guid userId)
    {
        var user = await _DBUsers.FindAsync(userId);

        if (user == null)
        {
            _logger.LogWarning($"Não foi possivel alterar usuario. '{userId}' Não encontrado");
            return;
        }
       
        user.IsValid = true;

        _DBUsers.Update(user);
    }

    public async Task ChangeUserPassword(Guid userId, string newPasswordHash)
    {
        var user = await _DBUsers.FindAsync(userId);

        if (user == null)
        {
            _logger.LogWarning($"Não foi possivel alterar usuario. '{userId}' Não encontrado");
            return;
        }

        user.HashPassword = newPasswordHash;

        _DBUsers.Update(user);
    }

    public async Task<Task> CreateUserAsync(domain.Entities.Users user)
    {
        await _DBUsers.AddAsync(user);

        await _dbConfig.SaveChangesAsync();

        return Task.CompletedTask;
    }


    public async Task DeactivateUserAsync(Guid userId)
    {
        var user = await _DBUsers.FindAsync(userId);

        if (user == null)
        {
            _logger.LogWarning($"Não foi possivel alterar usuario. '{userId}' Não encontrado");
            return;
        }

        user.IsValid = false;

        _DBUsers.Update(user);
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _DBUsers.FindAsync(userId);

        if (user == null)
        {
            _logger.LogWarning($"Não foi possivel alterar usuario. '{userId}' Não encontrado");
            return;
        }

        user.IsValid = false;

        _DBUsers.Update(user);
    }

    public Task UpdateUserProfile(Guid userId, string name)
    {
        throw new NotImplementedException();
    }
}
