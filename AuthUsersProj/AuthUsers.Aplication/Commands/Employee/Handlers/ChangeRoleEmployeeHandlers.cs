using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.domain.Entities;
using AuthUsers.domain.Interfaces.AuditLogs;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AuthUsers.Aplication.Commands.Employee.Handlers;

public class ChangeRoleEmployeeHandlers : IRequestHandler<ChangeRoleEmployeeCommands, Unit>
{

    private readonly ILogger<ChangeRoleEmployeeHandlers> _logger;

    private readonly IEmployeeRepositoryCommands _commands;

    private readonly IAuditlogsRepositoryCommands _commandsLog;

    private readonly IEmployeeRepositoryQuery _query;

    public ChangeRoleEmployeeHandlers(IEmployeeRepositoryQuery query, IEmployeeRepositoryCommands commands, ILogger<ChangeRoleEmployeeHandlers> logger, IAuditlogsRepositoryCommands commandsLog)
    {
        _query = query;
        _commands = commands;
        _logger = logger;
        _commandsLog = commandsLog;
    }

    public async Task<Unit> Handle(ChangeRoleEmployeeCommands request, CancellationToken cancellationToken)
    {
        var employee = await _query.GetEmployeeIdAsync(request.Id);

        if (employee == null)
            throw new NullReferenceException();

        employee.Position = request.Role;

        var log = new AuditLog
        {
            IdLog = Guid.NewGuid(),
            TableName = "Employee",
            RecordId = employee.IdEmployee,
            Action = "Path",
            DateLog = DateTimeOffset.UtcNow, // Ajuste para o fuso horário de Brasília
            PerformeBy = $"{request.UpdatebyId} | {employee.Nome} {employee.Surnames}",
            ChangesJson = JsonSerializer.Serialize(request)
        };

        await _commands.UpdateEmployeeProfile(employee);

        await _commandsLog.CreateAuditLog(log);

        return Unit.Value;
    }
}
