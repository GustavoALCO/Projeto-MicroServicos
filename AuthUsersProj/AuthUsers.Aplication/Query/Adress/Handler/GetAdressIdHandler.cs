using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Query.Adress.Handler;

public class GetAdressIdHandler : IRequestHandler<GetAdressIdQuery, domain.Entities.Adress?>
{
    private readonly domain.Interfaces.Adress.IAdressRepositoryQuery _adressRepositoryQuery;
    private readonly ILogger<GetAdressIdHandler> _logger;
    public GetAdressIdHandler(domain.Interfaces.Adress.IAdressRepositoryQuery adressRepositoryQuery, ILogger<GetAdressIdHandler> logger)
    {
        _adressRepositoryQuery = adressRepositoryQuery;
        _logger = logger;
    }
    public async Task<domain.Entities.Adress?> Handle(GetAdressIdQuery request, CancellationToken cancellationToken)
    {
        var adress = await _adressRepositoryQuery.GetByIdAsync(request.IdAdress);

        if (adress == null)
        {
            _logger.LogWarning("Endereço com ID {Id} Não encontrado.", request.IdAdress);
            throw new Exception("Endereço não encontrado");
        }

        _logger.LogInformation("Endereço com ID {Id} encontrado.", request.IdAdress);

        return adress;
    }
}