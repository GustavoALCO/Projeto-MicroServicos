using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Query.Users.Handler;

public class GetUserIdHandler : IRequestHandler<GetUserIdQuery, domain.Entities.Users?>
{
    private readonly domain.Interfaces.Users.IUserRepositoryQuery _userRepositoryQuery;
    private readonly ILogger<GetUserIdHandler> _logger;
    public GetUserIdHandler(domain.Interfaces.Users.IUserRepositoryQuery userRepositoryQuery, ILogger<GetUserIdHandler> logger)
    {
        _userRepositoryQuery = userRepositoryQuery;
        _logger = logger;
    }
    public async Task<domain.Entities.Users?> Handle(GetUserIdQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepositoryQuery.GetUserIdAsync(request.Id);

        //Verificando se o usuário foi encontrado
        if (users == null)
        {
            _logger.LogWarning("User com o ID {Id} não encontrado.", request.Id);
            throw new Exception($"User com o ID {request.Id} não encontrado.");
        }
        //Comunicando que o usuário foi encontrado com sucesso
        _logger.LogInformation("Usuario encontrado com o ID {Id}.", request.Id);
        //retorna o dados do usuário encontrado
        return users;
    }
}