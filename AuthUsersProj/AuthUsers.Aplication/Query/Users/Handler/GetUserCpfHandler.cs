using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Query.Users.Handler;

public class GetUserCpfHandler : IRequestHandler<GetUserCPFQuery, domain.Entities.Users?>
{
    private readonly domain.Interfaces.Users.IUserRepositoryQuery _userRepositoryQuery;
    private readonly ILogger<GetUserCpfHandler> _logger;
    public GetUserCpfHandler(domain.Interfaces.Users.IUserRepositoryQuery userRepositoryQuery, ILogger<GetUserCpfHandler> logger)
    {
        _userRepositoryQuery = userRepositoryQuery;
        _logger = logger;
    }
    public async Task<domain.Entities.Users?> Handle(GetUserCPFQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepositoryQuery.GetUserCPFAsync(request.CPF);
        // Verificando se o usuário foi encontrado
        if (user == null)
        {
            _logger.LogWarning("Usuário com CPF {Cpf} não encontrado.", request.CPF);
            throw new Exception($"Usuário com CPF {request.CPF} não encontrado. Tente passar neste formato 123.123.123-12");
        }
        // Comunicando que o usuário foi encontrado com sucesso
        _logger.LogInformation("Usuário encontrado com CPF {Cpf}.", request.CPF);
        
        // Retorna os dados do usuário encontrado
        return user;
    }
}

