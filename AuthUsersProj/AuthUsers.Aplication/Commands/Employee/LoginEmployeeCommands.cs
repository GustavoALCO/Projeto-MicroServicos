using MediatR;

namespace AuthUsers.Aplication.Commands.Employee;

public class LoginEmployeeCommands : IRequest<string>
{
    public required string Login { get; set; }

    public required string Password { get; set; }
}
