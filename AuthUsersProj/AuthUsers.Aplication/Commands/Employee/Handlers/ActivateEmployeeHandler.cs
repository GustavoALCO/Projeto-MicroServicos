using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.domain.Entities;
using AuthUsers.domain.Interfaces.AuditLogs;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AuthUsers.Aplication.Commands.Employee.Handlers;

public class ActivateEmployeeHandler : IRequestHandler<ActivateEmployeeCommands, Unit>
{
    private readonly IEmployeeRepositoryCommands _commands;

    private readonly IAuditlogsRepositoryCommands _auditlogsCommands;

    private readonly IEmployeeRepositoryQuery _query;

    private readonly ILogger<ActivateEmployeeHandler> _logger;

    public ActivateEmployeeHandler(ILogger<ActivateEmployeeHandler> logger, IEmployeeRepositoryQuery query, IEmployeeRepositoryCommands commands, IAuditlogsRepositoryCommands auditlogsCommands)
    {
        _logger = logger;
        _query = query;
        _commands = commands;
        _auditlogsCommands = auditlogsCommands;
    }

    public async Task<Unit> Handle(ActivateEmployeeCommands request, CancellationToken cancellationToken)
    {
        // Verifica se o Id do funcionário foi informado
        var employee = await _query.GetEmployeeIdAsync(request.IdEmployee);

        // Se o funcionário não for encontrado, lança uma exceção
        if (employee == null) 
            throw new NullReferenceException();

        // Ativa o Funcionario que estava desativado
        employee.IsActive = true;

        // Atualiza o funcionário no banco de dados
        var log = new AuditLog
        {
            IdLog = Guid.NewGuid(),
            TableName = "Employee",
            RecordId = employee.IdEmployee,
            Action = "Path",
            DateLog = DateTimeOffset.UtcNow, 
            PerformeBy = $"{request.UpdateById} | {employee.Nome} {employee.Surnames}",
            ChangesJson = JsonSerializer.Serialize(request)
        };

        // Registra o log de auditoria
        await _commands.UpdateEmployeeProfile(employee);

        // Cria o log de auditoria
        await _auditlogsCommands.CreateAuditLog(log);

        return Unit.Value;
    }
}
