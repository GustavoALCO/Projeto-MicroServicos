namespace AuthUsers.domain.Interfaces.Adress;

public interface IAdressRepositoryQuery
{
    public Task<Entities.Adress?> GetByIdAsync(int idAdress);

    public Task<IEnumerable<Entities.Adress>> GetAllAsync(Guid idUser, int page);
}
