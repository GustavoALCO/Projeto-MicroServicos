using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Query.Adress.Handler;

public class GetAllAdressHandler : IRequestHandler<GetAllAdressQuery, IEnumerable<domain.Entities.Adress>>
{
    private readonly domain.Interfaces.Adress.IAdressRepositoryQuery _adressRepositoryQuery;
    private readonly ILogger<GetAllAdressHandler> _logger;
    public GetAllAdressHandler(domain.Interfaces.Adress.IAdressRepositoryQuery adressRepositoryQuery, ILogger<GetAllAdressHandler> logger)
    {
        _adressRepositoryQuery = adressRepositoryQuery;
        _logger = logger;
    }
    public async Task<IEnumerable<domain.Entities.Adress>> Handle(GetAllAdressQuery request, CancellationToken cancellationToken)
    {
        var adress = await _adressRepositoryQuery.GetAllAsync(request.IdUser, request.Page);

        if (adress == null || !adress.Any())
        {
            _logger.LogWarning("No addresses found for user {UserId} on page {Page}", request.IdUser, request.Page);
            return Enumerable.Empty<domain.Entities.Adress>();
        }

        _logger.LogInformation("Retrieved {Count} addresses for user {UserId} on page {Page}", adress.Count(), request.IdUser, request.Page);

        return adress;
    }
}