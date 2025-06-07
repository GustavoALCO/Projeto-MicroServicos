namespace AuthUsers.Aplication.Interfaces;

public interface IPasswordHasher
{

    public string CreateHash(object user, string password);

    public bool ValidatePassword(object user, string hashedPassword, string providedPassword);
}
