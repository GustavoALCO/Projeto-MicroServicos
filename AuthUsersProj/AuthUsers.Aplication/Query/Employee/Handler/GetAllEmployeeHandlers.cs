using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.DTOs.Employee;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Query.Employee.Handler;

public class GetAllEmployeeHandlers : IRequestHandler<GetAllEmployeeQuery, IEnumerable<EmployeeDTO>>
{
    private readonly IEmployeeRepositoryQuery _query;

    private readonly ILogger<GetAllEmployeeHandlers> _logger;

    public GetAllEmployeeHandlers(ILogger<GetAllEmployeeHandlers> logger, IEmployeeRepositoryQuery query)
    {
        _logger = logger;
        _query = query;
    }

    public async Task<IEnumerable<EmployeeDTO>?> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
    {
        //Atribui o valor da paginação, se não for informado o valor será 1
        int page = request.Page;

         if (page <= 0)
            page = 1;

        //Multiplica por 10 para obter os registros da paginação
        page = page * 10;

        //Busca todos os funcionários, caso não tenha nenhum funcionário cadastrado retorna null
        var employee = await _query.GetAllEmployeesAsync(page);

        if (employee == null || !employee.Any())
        {
            _logger.LogWarning("Não a Funcionarios para listar", request.Page);
            return null;
        }

        //Converte a lista de funcionários para EmployeeDTO
        var employeeDto = employee.Select(e => new EmployeeDTO
        {
            IdEmployee = e.IdEmployee,
            Nome = e.Nome,
            Surnames = e.Surnames,
            Login = e.Login,
            Position = e.Position,
            IsActive = e.IsActive
        }).ToList();

        _logger.LogDebug(_logger.IsEnabled(LogLevel.Debug) ? "Lista de Funcionários retornada com sucesso" : "Lista de Funcionários retornada com sucesso", request.Page);

        //Retorna a lista de funcionários convertida
        return employeeDto;
    }
}
