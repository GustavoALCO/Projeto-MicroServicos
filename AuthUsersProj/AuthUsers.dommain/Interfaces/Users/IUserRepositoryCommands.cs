using AuthUsers.domain.Entities;

namespace AuthUsers.domain.Interfaces.Users;

public interface IUserRepositoryCommands
{
    public Task ChangeUserPassword(Guid userId, string newPasswordHash);

    public Task UpdateUserProfile(Guid userId, string name);

    public Task ActivateUserAsync(Guid userId);

    public Task DeactivateUserAsync(Guid userId);

    public Task<Task?> CreateUserAsync(Entities.Users user);

    public Task DeleteUserAsync(Guid userId);
}
