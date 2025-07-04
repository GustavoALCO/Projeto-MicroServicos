using MediatR;

namespace AuthUsers.Aplication.Query.Adress;

public class GetAllAdressQuery : IRequest<IEnumerable<domain.Entities.Adress>>
{
    public Guid IdUser { get; set; }

    public int Page { get; set; } = 1; // Default to page 1 if not specified
}
