namespace AuthEmployees.domain.Interfaces.Employee;

public interface IEmployeeRepositoryCommands
{
    public Task UpdateEmployeeProfile(AuthUsers.domain.Entities.Employee Employee);

    public Task CreateEmployeeAsync(AuthUsers.domain.Entities.Employee Employee);

    public Task DeleteEmployeeAsync(AuthUsers.domain.Entities.Employee Employee);
}
