using AuthEmployees.domain.Interfaces.Employee;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Query.Employee.Handler;

public class GetEmployeePositionHandler : IRequestHandler<GetEmployeePositionQuery, IEnumerable<AuthUsers.domain.Entities.Employee?>>
{
    private readonly IEmployeeRepositoryQuery _query;
    private readonly ILogger<GetEmployeePositionHandler> _logger;
    public GetEmployeePositionHandler(ILogger<GetEmployeePositionHandler> logger, IEmployeeRepositoryQuery query)
    {
        _logger = logger;
        _query = query;
    }
    public async Task<IEnumerable<AuthUsers.domain.Entities.Employee?>> Handle(GetEmployeePositionQuery request, CancellationToken cancellationToken)
    {
        //Atribui o valor da paginação, se não for informado o valor será 1
        int page = request.Page;
        if (page <= 0)
            page = 1;

        //Multiplica por 10 para obter os registros da paginação
        page = page * 10;

        //Busca os funcionários pela posição, caso não tenha nenhum funcionário cadastrado retorna null
        var employee = await _query.GetEmployeePositionAsync(request.Position, page);

        if (employee == null || !employee.Any())
        {
            _logger.LogWarning("Não existem funcionários com essa posição.", request.Position);

            throw new Exception("Não existem funcionários com essa posição.");
        }
        //Retorna a lista de funcionários encontrados
        return employee;
    }
}
