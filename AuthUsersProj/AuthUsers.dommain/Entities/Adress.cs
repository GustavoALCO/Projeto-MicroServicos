namespace AuthUsers.domain.Entities;

public abstract class Adress
{
    public required string HomeAdress { get; set; }

    public required int Number {  get; set; }

    public string? Complement { get; set; }

    public required string Cep { get; set; }
}
