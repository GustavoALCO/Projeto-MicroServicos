using ChatService.dommain.Entities;

namespace ChatService.dommain.Interfaces;

public interface IChatRepositoryQueries
{
    Task<Chat> BuscarChat(Guid id);

    Task<List<Chat>> BuscarChatsUsuario(Guid IdUser);

}
