using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.Commands.Employee;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Commands.Employee.Handlers;

public class DeleteEmployeeHandlers : IRequestHandler<DeleteEmployeeCommands, Unit>
{
    private readonly IEmployeeRepositoryCommands _commands;

    private readonly IEmployeeRepositoryQuery _query;

    private ILogger<DeleteEmployeeHandlers> _logger;

    public DeleteEmployeeHandlers(ILogger<DeleteEmployeeHandlers> logger, IEmployeeRepositoryQuery query, IEmployeeRepositoryCommands commands)
    {
        _logger = logger;
        _query = query;
        _commands = commands;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommands request, CancellationToken cancellationToken)
    {
        var employee = await _query.GetEmployeeIdAsync(request.Id);

        if (employee == null)
        {
            _logger.LogWarning($"Nao foi Encontrado nenhum Funcionario com o Id {request.Id}");
            throw new NullReferenceException();
        }

        await _commands.DeleteEmployeeAsync(employee);

        return Unit.Value;
    }
}
