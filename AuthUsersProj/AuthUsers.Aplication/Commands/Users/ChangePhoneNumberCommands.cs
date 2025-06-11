using MediatR;

namespace AuthUsers.Aplication.Commands.Users;

public class ChangePhoneNumberCommands : IRequest<Unit>
{
    public required Guid Id { get; set; }

    public required string Number { get; set; }
}
