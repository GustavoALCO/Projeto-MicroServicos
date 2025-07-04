using MediatR;

namespace AuthUsers.Aplication.Query.Users;

public class GetAllUsersQuery : IRequest<IEnumerable<domain.Entities.Users?>>
{
    public int Page { get; set; } = 1; // Default to page 1 if not specified

}
