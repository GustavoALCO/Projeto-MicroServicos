namespace AuthEmployees.domain.Interfaces.Employee;

public interface IEmployeeRepositoryCommands
{
    public Task ChangeEmployeePassword(Guid EmployeeId, string newPasswordHash);

    public Task UpdateEmployeeProfile(Guid EmployeeId, string name);

    public Task ActivateEmployeeAsync(Guid EmployeeId);

    public Task DeactivateEmployeeAsync(Guid EmployeeId);

    public Task<Task?> CreateEmployeeAsync(AuthUsers.domain.Entities.Employee Employee);

    public Task DeleteEmployeeAsync(Guid EmployeeId);
}
