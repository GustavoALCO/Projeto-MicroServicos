using AuthUsers.domain.Entities;

namespace AuthUsers.domain.Interfaces.AuditLogs;

public interface IAuditlogsRepositoryCommands
{
    Task CreateAuditLog(AuditLog auditLog);
}
