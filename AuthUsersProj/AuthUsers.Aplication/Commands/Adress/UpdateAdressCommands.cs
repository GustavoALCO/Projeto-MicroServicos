using MediatR;

namespace AuthUsers.Aplication.Commands.Adress;

public class UpdateAdressCommands : IRequest<Unit>
{
    public int Id { get; set; }

    public required string HomeAdress { get; set; }

    public required int Number { get; set; }

    public string? Complement { get; set; }

    public required string Cep { get; set; }
}