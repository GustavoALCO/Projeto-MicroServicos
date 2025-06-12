using MediatR;

namespace AuthUsers.Aplication.Commands.Users;

public class LoginUserCommands : IRequest<string>
{
    public required string Login {  get; set; }

    public required string Password { get; set; }

}
