using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.Interfaces;
using AuthUsers.domain.Entities;
using AuthUsers.domain.Interfaces.AuditLogs;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Text.Json;

namespace AuthUsers.Aplication.Commands.Employee.Handlers;

public class LoginEmployeeHandlers : IRequestHandler<LoginEmployeeCommands, string>
{
    private readonly ILogger<LoginEmployeeHandlers> _logger;

    private readonly IEmployeeRepositoryQuery _query;

    private readonly IAuditlogsRepositoryCommands _auditlogsRepositoryCommands;

    private readonly IPasswordHasher _hasher;

    private readonly IJWTService _jwt;

    private readonly AuditLog _auditLog;

    public LoginEmployeeHandlers(IEmployeeRepositoryQuery query, ILogger<LoginEmployeeHandlers> logger, IPasswordHasher hasher, IJWTService jwt, IAuditlogsRepositoryCommands auditlogsRepositoryCommands)
    {
        _query = query;
        _logger = logger;
        _hasher = hasher;
        _jwt = jwt;
        _auditlogsRepositoryCommands = auditlogsRepositoryCommands;
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

        //Se a senha for invalida, loga um aviso e lança uma exceção
        if (senha == false)
        {
            _logger.LogWarning("Senha Incorreta");

            await _auditlogsRepositoryCommands.CreateAuditLog(
            new AuditLog
            {
                IdLog = Guid.NewGuid(),
                TableName = "Employee",
                RecordId = employee.IdEmployee,
                Action = "Login",
                DateLog = DateTimeOffset.UtcNow, // Ajuste para o fuso horário de Brasília
                PerformeBy = $"{employee.Nome} {employee.Surnames}",
                ChangesJson = $"Usuario de login {employee.Nome}.{employee.Surnames} erro ao conectar"
            });

            throw new Exception("Login ou Senha incorreta");
        }

        var log = new AuditLog
        {
            IdLog = Guid.NewGuid(),
            TableName = "Employee",
            RecordId = employee.IdEmployee,
            Action = "Login",
            DateLog = DateTimeOffset.UtcNow, // Ajuste para o fuso horário de Brasília
            PerformeBy = $"{employee.Nome} {employee.Surnames}",
            ChangesJson = $"Usuario de login {employee.Nome}.{employee.Surnames} logado com sucesso"
        };

        await _auditlogsRepositoryCommands.CreateAuditLog(log);

        //Gera um jwt para o funcionario
        string jwt = _jwt.GerarTokenEmployee(employee.Login, employee.Position);

        return jwt;
    }
}
