using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.domain.Entities;
using AuthUsers.domain.Interfaces.AuditLogs;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AuthUsers.Aplication.Commands.Employee.Handlers;

public class DeleteEmployeeHandlers : IRequestHandler<DeleteEmployeeCommands, Unit>
{
    private readonly IEmployeeRepositoryCommands _commands;

    private readonly IAuditlogsRepositoryCommands _auditlogsCommands;

    private readonly IEmployeeRepositoryQuery _query;

    private ILogger<DeleteEmployeeHandlers> _logger;

    public DeleteEmployeeHandlers(ILogger<DeleteEmployeeHandlers> logger, IEmployeeRepositoryQuery query, IEmployeeRepositoryCommands commands, IAuditlogsRepositoryCommands auditlogsCommands)
    {
        _logger = logger;
        _query = query;
        _commands = commands;
        _auditlogsCommands = auditlogsCommands;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommands request, CancellationToken cancellationToken)
    {
        // Busca o funcionario pelo Id informado na requisicao
        var employee = await _query.GetEmployeeIdAsync(request.Id);

        // Se o funcionario nao for encontrado, loga um aviso e lança uma exceção
        if (employee == null)
        {
            _logger.LogWarning($"Nao foi Encontrado nenhum Funcionario com o Id {request.Id}");
            throw new NullReferenceException();
        }

        // Loga a acao de exclusao do funcionario
        await _commands.DeleteEmployeeAsync(employee);

        // Log de informacao para registrar a exclusao do funcionario
        var log = new AuditLog
        {
            IdLog = Guid.NewGuid(),
            TableName = "Employee",
            RecordId = employee.IdEmployee,
            Action = "Delete",
            DateLog = DateTimeOffset.UtcNow, // Ajuste para o fuso horário de Brasília
            PerformeBy = $"{request.UpdateById} | {employee.Nome} {employee.Surnames}",
            ChangesJson = JsonSerializer.Serialize(request)
        };

        // Cria o log de auditoria
        await _auditlogsCommands.CreateAuditLog(log);

        return Unit.Value;
    }
}
