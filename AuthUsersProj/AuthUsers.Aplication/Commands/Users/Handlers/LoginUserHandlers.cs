using AuthUsers.Aplication.Interfaces;
using AuthUsers.domain.Interfaces.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Commands.Users.Handlers;

public class LoginUserHandlers : IRequestHandler<LoginUserCommands, string>
{
    private readonly ILogger _logger;

    private readonly IUserRepositoryQuery _query;

    private readonly IPasswordHasher _hasher;

    private readonly IJWTService _jwt;
    public LoginUserHandlers(IUserRepositoryQuery query, ILogger logger, IPasswordHasher hasher, IJWTService jwt)
    {
        _query = query;
        _logger = logger;
        _hasher = hasher;
        _jwt = jwt;
    }
    public async Task<string> Handle(LoginUserCommands request, CancellationToken cancellationToken)
    {
        //Busca no banco de dados se existe algum login 
        var employee = await _query.GetUserEmailAsync(request.Login);

        if (employee == null)
        {
            _logger.LogWarning($"Usuario de Login : {request.Login} não foi encontrado");
            throw new Exception("Login ou Senha incorreta");
        }

        //Verifica a senha passada é valida 
        var senha = _hasher.ValidatePassword(employee, employee.HashPassword, request.Password);

        if (senha == false)
        {
            _logger.LogWarning("Senha Incorreta");
            throw new Exception("Login ou Senha incorreta");
        }

        //Gera um jwt para o funcionario
        string jwt = _jwt.GerarTokenUser(employee.Email, employee.Name, employee.Surname);

        return jwt;
    }
}
