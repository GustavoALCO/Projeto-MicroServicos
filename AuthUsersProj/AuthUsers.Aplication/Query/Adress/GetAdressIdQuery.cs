using MediatR;

namespace AuthUsers.Aplication.Query.Adress;

public class GetAdressIdQuery : IRequest<domain.Entities.Adress?>
{
    public int IdAdress { get; set; }
    
}