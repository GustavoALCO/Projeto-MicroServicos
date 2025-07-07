using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.Aplication.DTOs.Employee;
using AuthUsers.domain.Entities;
using AuthUsers.domain.Interfaces.AuditLogs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthUsers.Aplication.Query.Employee.Handler;

public class GetAllEmployeeAdminHandler : IRequestHandler<GetAllEmployeeAdminQuery, IEnumerable<EmployeeAdminDTO>>
{
    private readonly IEmployeeRepositoryQuery _query;

    private readonly IAuditLogRepositoryQuery _auditLogQuery;

    private readonly ILogger<GetAllEmployeeHandlers> _logger;

    public GetAllEmployeeAdminHandler(ILogger<GetAllEmployeeHandlers> logger, IEmployeeRepositoryQuery query, IAuditLogRepositoryQuery auditLogQuery)
    {
        _logger = logger;
        _query = query;
        _auditLogQuery = auditLogQuery;
    }

    public async Task<IEnumerable<EmployeeAdminDTO>> Handle(GetAllEmployeeAdminQuery request, CancellationToken cancellationToken)
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

        var logs = await _auditLogQuery.GetAllLogs();

        //Converte a lista de funcionários para EmployeeAdminDTO
        var employeeDto = employee.Select(e => new EmployeeAdminDTO
        {
            IdEmployee = e.IdEmployee,
            Nome = e.Nome,
            Surnames = e.Surnames,
            Login = e.Login,
            Position = e.Position,
            IsActive = e.IsActive,
            Audits = logs.Where(l => l.RecordId == e.IdEmployee).ToList()
        }).ToList();

        _logger.LogDebug(_logger.IsEnabled(LogLevel.Debug) ? "Lista de Funcionários retornada com sucesso" : "Lista de Funcionários retornada com sucesso", request.Page);

        //Retorna a lista de funcionários convertida
        return employeeDto;
    }
}
