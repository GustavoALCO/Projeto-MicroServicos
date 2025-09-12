using ChatService.dommain.Entities;

namespace ChatService.dommain.Interfaces;

public interface IChatRepositoryCommands
{
    Task CreateChat(Chat chat);

    Task UpdateChat(Chat chat);


}
