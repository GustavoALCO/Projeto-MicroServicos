using MediatR;

namespace AuthUsers.Aplication.Commands.Adress;

public class CreateAdressCommands : IRequest<Unit>
{
    public Guid IdUser { get; set; }

    public required string HomeAdress { get; set; }

    public required int Number { get; set; }

    public string? Complement { get; set; }

    public required string Cep { get; set; }
}
