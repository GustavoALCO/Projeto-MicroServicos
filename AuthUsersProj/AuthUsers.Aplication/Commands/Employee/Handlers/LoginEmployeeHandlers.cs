using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace AuthUsers.Aplication.Commands.Employee.Handlers;

public class LoginEmployeeHandlers : IRequestHandler<LoginEmployeeCommands, string>
{
    private readonly ILogger _logger;

    private readonly IEmployeeRepositoryQuery _query;

    private readonly IPasswordHasher _hasher;

    private readonly IJWTService _jwt;
    public LoginEmployeeHandlers(IEmployeeRepositoryQuery query, ILogger logger, IPasswordHasher hasher, IJWTService jwt)
    {
        _query = query;
        _logger = logger;
        _hasher = hasher;
        _jwt = jwt;
    }

    public async Task<string> Handle(LoginEmployeeCommands request, CancellationToken cancellationToken)
    {
        //Busca no banco de dados se existe algum login 
        var employee = await _query.GetEmployeeLoginAsync(request.Login);

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
        string jwt = _jwt.GerarTokenEmployee(employee.Login, employee.Position);

        return jwt;
    }
}
