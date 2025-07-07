using AuthUsers.domain.Entities;
using AuthUsers.domain.Interfaces.AuditLogs;
using AuthUsers.infra.DbConfig;
using Microsoft.Extensions.Logging;

namespace AuthUsers.infra.Repositories.AuditLogs;

public class AuditLogsRepositoryCommands : IAuditlogsRepositoryCommands
{
    private readonly ContextDB _db;

    private readonly ILogger<AuditLogsRepositoryCommands> _logger;

    public AuditLogsRepositoryCommands(ContextDB db, ILogger<AuditLogsRepositoryCommands> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task CreateAuditLog(AuditLog auditLog)
    {
        await _db.AuditLog.AddAsync(auditLog);

        await _db.SaveChangesAsync();

        _logger.LogInformation($"Log Criado {auditLog}");
    }
}
