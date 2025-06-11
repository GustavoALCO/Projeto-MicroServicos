using AuthUsers.domain.Entities;

namespace AuthUsers.domain.Interfaces.Users;

public interface IUserRepositoryCommands
{
    public Task UpdateUserAync(Entities.Users user);

    public Task CreateUserAsync(Entities.Users user);

    public Task DeleteUserAsync(Entities.Users user);
}
