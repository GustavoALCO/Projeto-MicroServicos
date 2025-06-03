using AuthEmployees.domain.Interfaces.Employee;

namespace AuthUsers.infra.Repositories.Employee;

public class EmployeeRepositoryCommands : IEmployeeRepositoryCommands
{
    public Task ActivateEmployeeAsync(Guid EmployeeId)
    {
        throw new NotImplementedException();
    }

    public Task ChangeEmployeePassword(Guid EmployeeId, string newPasswordHash)
    {
        throw new NotImplementedException();
    }

    public Task<Task?> CreateEmployeeAsync(domain.Entities.Employee Employee)
    {
        throw new NotImplementedException();
    }

    public Task DeactivateEmployeeAsync(Guid EmployeeId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEmployeeAsync(Guid EmployeeId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateEmployeeProfile(Guid EmployeeId, string name)
    {
        throw new NotImplementedException();
    }
}
