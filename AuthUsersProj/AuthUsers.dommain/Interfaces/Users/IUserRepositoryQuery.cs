namespace AuthUsers.domain.Interfaces.Users;

public interface IUserRepositoryQuery
{
    public Task<IEnumerable<Entities.Users?>> GetAllUsersAsync(int page);

    public Task<Entities.Users?> GetUserIdAsync(Guid Id);

    public Task<Entities.Users?> GetUserEmailAsync(string email);

    public Task<IEnumerable<Entities.Users?>> UserFilterAsync(IQueryable<Entities.Users> users, int page);

    public IQueryable<Entities.Users?> UserQuerable();

    public Task<Entities.Users> GetUserCPFAsync(string cpf);
}
