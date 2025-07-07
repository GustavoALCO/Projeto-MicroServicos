using AuthUsers.domain.Entities;

namespace AuthUsers.domain.Interfaces.AuditLogs;

public interface IAuditLogRepositoryQuery
{
    Task<IEnumerable<AuditLog>> GetAllLogs();

    Task<AuditLog> GetLogs(Guid Id);
}
