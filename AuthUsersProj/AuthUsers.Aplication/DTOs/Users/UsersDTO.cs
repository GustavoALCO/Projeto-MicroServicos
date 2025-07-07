using AuthUsers.Aplication.DTOs.Adress;
using AuthUsers.domain.Entities;

namespace AuthUsers.Aplication.DTOs.Users;

public class UsersDTO
{
    public Guid IdUser { get; set; }

    public required string Name { get; set; }

    public required string Surname { get; set; }

    public required string PhoneNumber { get; set; }

    public required string Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool IsValid { get; set; }

    public List<AdressDTO> Adress { get; set; }
}
