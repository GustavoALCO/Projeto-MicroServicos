using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Commands.Employee.Handlers;

public class ActivateEmployeeHandler : IRequestHandler<ActivateEmployeeCommands, Unit>
{
    private readonly IEmployeeRepositoryCommands _commands;

    private readonly IEmployeeRepositoryQuery _query;

    private readonly ILogger _logger;

    public ActivateEmployeeHandler(ILogger logger, IEmployeeRepositoryQuery query, IEmployeeRepositoryCommands commands)
    {
        _logger = logger;
        _query = query;
        _commands = commands;
    }

    public async Task<Unit> Handle(ActivateEmployeeCommands request, CancellationToken cancellationToken)
    {

        var employee = await _query.GetEmployeeIdAsync(request.IdEmployee);

        if (employee == null) 
            throw new NullReferenceException();

        employee.IsActive = true;

        employee.Audits.Add(new AuditLog
        {
            Id = request.UpdateById,
            Action = "ActivateEmployee",
            PerformedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)),
            ChangesJson = request.json
        }); 

        await _commands.UpdateEmployeeProfile(employee);

        return Unit.Value;
    }
}
