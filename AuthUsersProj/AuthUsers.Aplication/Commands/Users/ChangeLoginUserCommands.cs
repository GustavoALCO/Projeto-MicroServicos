using MediatR;

namespace AuthUsers.Aplication.Commands.Users;

public class ChangeLoginUserCommands : IRequest<Unit>
{
    public required string Email { get; set; }

    public string ChangeEmail { get; set; }

    public string Password { get; set; }
}
