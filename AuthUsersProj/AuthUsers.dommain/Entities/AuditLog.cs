namespace AuthUsers.domain.Entities;

public class AuditLog
{
    public required Guid Id { get; set; }
    public required string Action { get; set; }
    public required DateTimeOffset PerformedAt { get; set; }
    public string? ChangesJson { get; set; }


}

