using MediatR;

namespace AuthUsers.Aplication.Query.Users;

public class GetUserCPFQuery : IRequest<domain.Entities.Users?>
{
    public string CPF { get; set; }
    
}