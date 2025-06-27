using AuthEmployees.domain.Interfaces.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthUsers.infra.Repositories.Employee;

public class EmployeeRepositoryQuery : IEmployeeRepositoryQuery
{
    private readonly DbConfig.ContextDB _dbConfig;

    private readonly DbSet<domain.Entities.Employee> _DbEmployee;

    private readonly ILogger<EmployeeRepositoryQuery> _logger;

    public EmployeeRepositoryQuery(DbConfig.ContextDB dbConfig, ILogger<EmployeeRepositoryQuery> logger)
    {

        _dbConfig = dbConfig;
        _DbEmployee = _dbConfig.Employee;
        _logger = logger;
    }

    public async Task<IEnumerable<domain.Entities.Employee>?> GetEmployeeFilterAsync(IQueryable<domain.Entities.Employee> Employees, int page)
    {
        var employee = await Employees.Take(page).ToListAsync();

        if (!employee.Any())
        {
            _logger.LogWarning("Erro ao listar Funcionarios,\n Nao foi encontrado nenhum pelo filtro");
            return null;
        }

        return employee;
    }

    public IQueryable<domain.Entities.Employee?> GetEmployeeQuerable()
    {
        return _DbEmployee.AsQueryable();
    }

    public async Task<IEnumerable<domain.Entities.Employee?>> GetAllEmployeesAsync(int page)
    {
        var employee = await _DbEmployee.Take(page).ToListAsync();

        return employee;
    }

    public async Task<IEnumerable<domain.Entities.Employee?>?> GetEmployeePositionAsync(string position, int page)
    {
        var employee = await _DbEmployee.Where(x => x.Login.ToLower() == position.ToLower()).Take(page).ToListAsync();

        if (!employee.Any())
        {
            _logger.LogWarning("Erro ao listar Funcionarios,\n Nao foi encontrado nenhum pelo Cargo selecionado");
            return null;
        }

        return employee;
    }

    public async Task<domain.Entities.Employee?> GetEmployeeIdAsync(Guid Id)
    {
        var employee = await _DbEmployee.FirstOrDefaultAsync(x => x.IdEmployee == Id);

        if (employee == null)
        {
            _logger.LogWarning($"Erro ao listar Funcionario,\n Nao foi encontrado nenhum pelo Id : {Id}");
            return null;
        }
        return employee;
    }

    public async Task<domain.Entities.Employee?> GetEmployeeLoginAsync(string Login)
    {
        var employee = await _DbEmployee.FirstOrDefaultAsync(x => x.Login.ToLower() == Login.ToLower());

        if (employee == null)
        {
            _logger.LogWarning($"Erro ao listar Funcionario,\n Nao foi encontrado nenhum pelo Login : {Login}");
            return null;
        }

        return employee;
    }

    public async Task<IEnumerable<domain.Entities.Employee?>> GetEmployeesLoginAsync(string Login)
    {
        var employee = await _DbEmployee.Where(x => x.Login.ToUpper()
                                                                    .Contains(Login.ToUpper()
                                                                    ))
                                                                    .ToListAsync();

        if (employee == null)
        {
            _logger.LogWarning($"Erro ao listar Funcionarios ,\n Nao foi encontrado nenhum pelo Login : {Login}");
            return null;
        }

        return employee;
    }
}
