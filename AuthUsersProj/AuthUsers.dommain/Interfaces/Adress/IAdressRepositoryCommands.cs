namespace AuthUsers.domain.Interfaces.Adress;

public interface IAdressRepositoryCommands
{
    Task AddAsync(Entities.Adress adress);
    Task UpdateAsync(Entities.Adress adress);
    Task DeleteAsync(Entities.Adress idAdress);
}
