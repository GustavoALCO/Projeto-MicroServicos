using AuthEmployees.domain.Interfaces.Employee;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Query.Employee.Handler;

public class GetEmployeeIdHandler : IRequestHandler<GetEmployeeId, AuthUsers.domain.Entities.Employee?>
{
    private readonly IEmployeeRepositoryQuery _query;

    private readonly ILogger<GetEmployeeIdHandler> _logger;

    public GetEmployeeIdHandler(ILogger<GetEmployeeIdHandler> logger, IEmployeeRepositoryQuery query)
    {
        _logger = logger;
        _query = query;
    }

    public async Task<AuthUsers.domain.Entities.Employee?> Handle(GetEmployeeId request, CancellationToken cancellationToken)
    {
        var employee = await _query.GetEmployeeIdAsync(request.Id);

        if (employee == null)
        {
            _logger.LogWarning("Funcionário não encontrado com o ID: {Id}", request.Id);
            throw new Exception($"Funcionário com ID {request.Id} não encontrado.");
        }

        _logger.LogInformation("Funcionário encontrado com o ID: {Id}", request.Id);

        return employee;
    }
}