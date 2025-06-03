namespace AuthUsers.domain.Entities;

public abstract class AuditLog
{
    public required Guid Id { get; set; }
    public required string Action { get; set; }
    public required DateTime PerformedAt { get; set; }
    public string? ChangesJson { get; set; }
}
