using AuthUsers.domain.Entities;
using AuthUsers.domain.Interfaces.AuditLogs;
using AuthUsers.infra.DbConfig;
using Microsoft.EntityFrameworkCore;

namespace AuthUsers.infra.Repositories.AuditLogs;

public class AuditLogsRepositoryQuery : IAuditLogRepositoryQuery
{
    private readonly ContextDB _db;

    public AuditLogsRepositoryQuery(ContextDB db)
    {
        _db = db;
    }

    public async Task<IEnumerable<AuditLog>> GetAllLogs()
    {
        return await _db.AuditLog.ToListAsync();
    }

    public async Task<AuditLog> GetLogs(Guid Id)
    {
        return await _db.AuditLog.FirstOrDefaultAsync(x => x.IdLog == Id);
    }
}
