namespace AuthUsers.domain.Interfaces.Adress;

public interface IAdressRepositoryQuery
{
    public Task<Entities.Adress?> GetByIdAsync(int id);

    public Task<IEnumerable<Entities.Adress>> GetAllAsync(Guid idAdress, int page);
}
