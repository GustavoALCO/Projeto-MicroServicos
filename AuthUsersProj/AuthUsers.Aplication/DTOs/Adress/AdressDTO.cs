namespace AuthUsers.Aplication.DTOs.Adress;

public class AdressDTO
{
    public int Id { get; set; }

    public Guid IdUser { get; set; }

    public required string HomeAdress { get; set; }

    public required int Number { get; set; }

    public string? Complement { get; set; }

    public required string Cep { get; set; }
}
