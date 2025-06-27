using System.ComponentModel.DataAnnotations;

namespace AuthUsers.domain.Entities;

public class Adress
{
    [Key]
    public int Id { get; set; }

    public Guid IdUser { get; set; }

    public required string HomeAdress { get; set; }

    public required int Number {  get; set; }

    public string? Complement { get; set; }

    public required string Cep { get; set; }

    public Users Users { get; set; }
}
