using MediatR;

namespace AuthUsers.Aplication.Commands.Employee;

public class ActivateEmployeeCommands : IRequest<Unit>
{
    public required Guid IdEmployee { get; set; }

    public required Guid UpdateById {  get; set; }

    public string json { get; set; }
}
