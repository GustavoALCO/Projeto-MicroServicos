using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.Commands.Employee;
using AuthUsers.domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Commands.Employee.Handlers;

public class ChangeRoleEmployeeHandlers : IRequestHandler<ChangeRoleEmployeeCommands, Unit>
{

    private readonly ILogger _logger;

    private readonly IEmployeeRepositoryCommands _commands;

    private readonly IEmployeeRepositoryQuery _query;

    

    public async Task<Unit> Handle(ChangeRoleEmployeeCommands request, CancellationToken cancellationToken)
    {
        var employee = await _query.GetEmployeeIdAsync(request.Id);

        if (employee == null)
            throw new NullReferenceException();

        employee.Position = request.Role;

        employee.Audits.Add(new AuditLog
        {
            Id = request.UpdatebyId,
            Action = "ChangeRole",
            PerformedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)),
            ChangesJson = request.json
        });

        await _commands.UpdateEmployeeProfile(employee);

        return Unit.Value;
    }
}
