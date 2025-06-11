using MediatR;

namespace AuthUsers.Aplication.Commands.Users;

public class EmailConfirmedCommands : IRequest<Unit>
{
    public Guid Id { get; set; }
}
