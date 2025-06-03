namespace AuthEmployees.domain.Interfaces.Employee;

public interface IEmployeeRepositoryQuery
{
    public Task<IEnumerable<AuthUsers.domain.Entities.Employees?>> GetAllEmployeesAsync(int page);

    public Task<AuthUsers.domain.Entities.Employees?> GetEmployeeIdAsync(Guid Id);

    public Task<AuthUsers.domain.Entities.Employees?> GetEmployeeEmailAsync(string email);

    public Task<IEnumerable<AuthUsers.domain.Entities.Employees?>> EmployeeFilterAsync(IQueryable<AuthUsers.domain.Entities.Employees> Employees, int page);

    public IQueryable<AuthUsers.domain.Entities.Employees?> EmployeeQuerable();
}
