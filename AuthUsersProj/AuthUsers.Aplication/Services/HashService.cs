using Microsoft.AspNetCore.Identity;

namespace AuthUsers.Aplication.Services;

public class HashService
{

    private readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();


    public string CreateHash(object user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool ValidatePassword(object user, string hashedPassword, string providedPassword)
    {
        var isvalid = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        //Para Usar a verificação da Senha deve se Passar o Usuario, SenhaHash e request da senha

        return isvalid == PasswordVerificationResult.Success;
        //Retorna True ou False  
    }
}
