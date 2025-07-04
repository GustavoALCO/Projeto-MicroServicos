using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Query.Users.Handler;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<domain.Entities.Users?>>
{
    private readonly domain.Interfaces.Users.IUserRepositoryQuery _userRepositoryQuery;

    private readonly ILogger<GetAllUsersHandler> _logger;
    public GetAllUsersHandler(domain.Interfaces.Users.IUserRepositoryQuery userRepositoryQuery, ILogger<GetAllUsersHandler> logger)
    {
        _userRepositoryQuery = userRepositoryQuery;
        _logger = logger;
    }
    public async Task<IEnumerable<domain.Entities.Users?>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepositoryQuery.GetAllUsersAsync(request.Page);

        //Verifica se existe usuarios cadastrados
        if (users == null || !users.Any())
        {
            _logger.LogWarning("Não Existe Usuarios Cadastrados.");
            return Enumerable.Empty<domain.Entities.Users?>();
        }

        // retorna o valor dos usuarios cadastrados
        return users;
    }
}
