namespace ChatService.dommain.Entities;

public class UsersChat
{
    public required Guid RecibeUser {  get; set; }

    public required Guid SendUser { get; set; }
}
