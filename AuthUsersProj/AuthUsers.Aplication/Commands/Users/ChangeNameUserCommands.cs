using MediatR;

namespace AuthUsers.Aplication.Commands.Users;

public class ChangeNameUserCommands : IRequest<Unit>
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Surname { get; set; }

}
