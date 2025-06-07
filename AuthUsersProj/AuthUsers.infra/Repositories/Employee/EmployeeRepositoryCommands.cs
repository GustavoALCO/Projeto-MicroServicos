using AuthEmployees.domain.Interfaces.Employee;
using AuthUsers.domain.Entities;
using AuthUsers.domain.Interfaces.Users;
using AuthUsers.infra.DbConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthUsers.infra.Repositories.Employee;

public class EmployeeRepositoryCommands : IEmployeeRepositoryCommands
{
    private readonly DbConfig.DbConfig _dbConfig;

    private readonly DbSet<domain.Entities.Employee> _DbEmployee;

    private readonly IEmployeeRepositoryQuery _employee;

    private readonly ILogger _logger;

    public EmployeeRepositoryCommands(IEmployeeRepositoryQuery employee, DbSet<domain.Entities.Employee> dbEmployee, DbConfig.DbConfig dbConfig, ILogger logger)
    {
        _employee = employee;
        _DbEmployee = dbEmployee;
        _dbConfig = dbConfig;
        _logger = logger;
    }


    public async Task CreateEmployeeAsync(domain.Entities.Employee Employee)
    {
        await _DbEmployee.AddAsync(Employee);

        await _dbConfig.SaveChangesAsync();

        _logger.LogInformation($"Funcionario {Employee.Nome} {Employee.Surnames} Criado");

    }

    public async Task DeleteEmployeeAsync(domain.Entities.Employee employee)
    {

        _DbEmployee.Remove(employee);

        await _dbConfig.SaveChangesAsync();

        _logger.LogInformation("Funcionario Excluido com Sucesso");
    }

    public async Task UpdateEmployeeProfile(domain.Entities.Employee employee)
    {
        _DbEmployee.Update(employee);

        await _dbConfig.SaveChangesAsync();

        _logger.LogInformation("Funcionario Atualizado com Sucesso");

    }
}
