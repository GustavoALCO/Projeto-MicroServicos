using MediatR;

namespace AuthUsers.Aplication.Commands.Adress;

public class DeleteAdressCommands : IRequest<Unit>
{
    public int IdAdress { get; set; }
}
