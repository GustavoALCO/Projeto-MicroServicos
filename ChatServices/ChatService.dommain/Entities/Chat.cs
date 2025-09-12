namespace ChatService.dommain.Entities;

public class Chat
{
    public Guid ChatId { get; set; }

    public Guid AdsId { get; set; }

    public List<Mensages> Mensages { get; set; }

    public UsersChat User { get; set; }

}
