using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Query.Users.Handler;

public class GetUserEmailHandler : IRequestHandler<GetUserEmailQuery, domain.Entities.Users?>
{
    private readonly domain.Interfaces.Users.IUserRepositoryQuery _userRepositoryQuery;
    private readonly ILogger<GetUserEmailHandler> _logger;
    public GetUserEmailHandler(domain.Interfaces.Users.IUserRepositoryQuery userRepositoryQuery, ILogger<GetUserEmailHandler> logger)
    {
        _userRepositoryQuery = userRepositoryQuery;
        _logger = logger;
    }
    public async Task<domain.Entities.Users?> Handle(GetUserEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepositoryQuery.GetUserEmailAsync(request.Email);
        // Verificando se o usuário foi encontrado
        if (user == null)
        {
            _logger.LogWarning("Usuário com o email {Email} não encontrado.", request.Email);
            throw new Exception($"Usuário com o email {request.Email} não encontrado. Tente passar um email válido.");
        }
        // Comunicando que o usuário foi encontrado com sucesso
        _logger.LogInformation("Usuário encontrado com o email {Email}.", request.Email);
        
        // Retorna os dados do usuário encontrado
        return user;
    }
}
