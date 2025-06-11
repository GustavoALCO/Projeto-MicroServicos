using MediatR;

namespace AuthUsers.Aplication.Commands.Users;

public class DeleteUserCommands : IRequest<Unit>
{
    public required Guid Id { get; set; }
}
