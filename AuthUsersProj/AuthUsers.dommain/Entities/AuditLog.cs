using System.ComponentModel.DataAnnotations;

namespace AuthUsers.domain.Entities;

public class AuditLog
{
    [Key]
    public required Guid IdLog { get; set; }

    //Informa para que tabela o log foi feito
    public required string TableName { get; set; }

    //Informa o Id do registro que foi alterado
    public required Guid RecordId { get; set; }

    //Informa a ação que foi feita no registro (Create, Update, Delete)
    public required string Action { get; set; }

    //Informa a data e hora do log
    public required DateTimeOffset DateLog { get; set; }

    //informa o Id e Nome do usuário que fez a ação
    public required string PerformeBy { get; set; }

    //Informa o Json com as alterações feitas no registro, se houver
    public string? ChangesJson { get; set; }
}

