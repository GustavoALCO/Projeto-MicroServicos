namespace AuthEmployees.domain.Interfaces.Employee;

public interface IEmployeeRepositoryQuery
{
    public Task<IEnumerable<AuthUsers.domain.Entities.Employee?>> GetAllEmployeesAsync(int page);

    public Task<AuthUsers.domain.Entities.Employee?> GetEmployeeIdAsync(Guid Id);

    public Task<IEnumerable<AuthUsers.domain.Entities.Employee?>> GetEmployeePositionAsync(string Position, int page);

    public Task<IEnumerable<AuthUsers.domain.Entities.Employee?>> GetEmployeeFilterAsync(IQueryable<AuthUsers.domain.Entities.Employee> Employees, int page);

    public IQueryable<AuthUsers.domain.Entities.Employee?> GetEmployeeQuerable();

    public Task<AuthUsers.domain.Entities.Employee> GetEmployeeLoginAsync(string Login);

    public Task<IEnumerable<AuthUsers.domain.Entities.Employee>> GetEmployeesLoginAsync(string Login);
}
