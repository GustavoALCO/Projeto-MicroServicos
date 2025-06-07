using MediatR;

namespace AuthUsers.Aplication.Commands.Employee;

public class ChangeRoleEmployeeCommands : IRequest<Unit>
{
    public required Guid Id { get; set; }

    public required string Role { get; set; }

    public required Guid UpdatebyId { get; set; }

    public string json { get; set; }
}
