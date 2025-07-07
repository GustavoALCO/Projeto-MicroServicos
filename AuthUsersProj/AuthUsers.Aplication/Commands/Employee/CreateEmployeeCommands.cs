using MediatR;

namespace AuthUsers.Aplication.Commands.Employee;

public class CreateEmployeeCommands : IRequest<Unit>
{
    public required string Nome { get; set; }

    public required string Surnames { get; set; }

    public required string HashPassword { get; set; }

    public required string Position { get; set; }

    public Guid CreateById { get; set; }

}
