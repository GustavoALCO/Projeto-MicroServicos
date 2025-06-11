using MediatR;

namespace AuthUsers.Aplication.Commands.Users;

public class CreateUsersCommands : IRequest<Unit>
{
    public required string Name { get; set; }

    public required string Surname { get; set; }

    public required string Cpf { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Email { get; set; }

    public required string HashPassword { get; set; }

    public string? json { get; set; }
}
