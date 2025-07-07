using MediatR;

namespace AuthUsers.Aplication.Commands.Employee;

public class DeleteEmployeeCommands : IRequest<Unit>
{
    public required Guid Id { get; set; }

    public required Guid UpdateById { get; set; }

}
