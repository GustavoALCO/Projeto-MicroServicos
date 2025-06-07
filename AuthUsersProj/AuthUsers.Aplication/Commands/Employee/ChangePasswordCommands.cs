using MediatR;

namespace AuthUsers.Aplication.Commands.Employee;

public class ChangePasswordCommands : IRequest<Unit>
{
    public required Guid Id { get; set; }

    public required string Password { get; set; }

    public required Guid UpdatebyId { get; set; }

}
