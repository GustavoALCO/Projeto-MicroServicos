using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.domain.Entities;
using AuthUsers.domain.Interfaces.AuditLogs;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Windows.Input;

namespace AuthUsers.Aplication.Commands.Employee.Handlers;

public class DeactivateEmployeeHandlers : IRequestHandler<DeactivateEmployeeCommands, Unit>
{
    private readonly IEmployeeRepositoryCommands _commands;

    private readonly IAuditlogsRepositoryCommands _auditLogCommands;

    private readonly IEmployeeRepositoryQuery _query;

    private readonly ILogger<DeactivateEmployeeHandlers> _logger;

    public DeactivateEmployeeHandlers(ILogger<DeactivateEmployeeHandlers> logger, IEmployeeRepositoryQuery query, IEmployeeRepositoryCommands commands, IAuditlogsRepositoryCommands auditLogCommands)
    {
        _logger = logger;
        _query = query;
        _commands = commands;
        _auditLogCommands = auditLogCommands;
    }

    public async Task<Unit> Handle(DeactivateEmployeeCommands request, CancellationToken cancellationToken)
    {
        // Verifica se o ID do funcionário foi fornecido
        var employee = await _query.GetEmployeeIdAsync(request.IdEmployee);

        // Log de depuração para verificar se o funcionário foi encontrado
        if (employee == null)
            throw new NullReferenceException();

        // Log de informação para registrar a desativação do funcionário
        employee.IsActive = false;

        _logger.LogInformation("Desativando funcionário com ID: {IdEmployee}", employee.IdEmployee);

        // Log de informação para registrar a desativação do funcionário
        var log = new AuditLog
        {
            IdLog = Guid.NewGuid(),
            TableName = "Employee",
            RecordId = employee.IdEmployee,
            Action = "Create",
            DateLog = DateTimeOffset.UtcNow, // Ajuste para o fuso horário de Brasília
            PerformeBy = $"{request.UpdateById} | {employee.Nome} {employee.Surnames}",
            ChangesJson = JsonSerializer.Serialize(request)
        };

        // Atualiza o perfil do funcionário
        await _commands.UpdateEmployeeProfile(employee);

        // Cria o log de auditoria
        await _auditLogCommands.CreateAuditLog(log);

        return Unit.Value;
    }
}
