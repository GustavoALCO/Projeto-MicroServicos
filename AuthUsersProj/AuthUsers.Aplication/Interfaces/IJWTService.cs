namespace AuthUsers.Aplication.Interfaces;

public interface IJWTService
{
    public string GerarTokenUser(string email, string name, string surname);

    public string GerarTokenEmployee(string email, string position);
}
