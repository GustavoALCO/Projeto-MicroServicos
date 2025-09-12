using AuthUsers.infra.DbConfig;
using ChatService.dommain.Entities;
using ChatService.dommain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChatService.infra.Repositories.ChatCommands;

public class ChatRepositoryQueries : IChatRepositoryQueries
{
    private readonly ILogger<ChatRepositoriesCommands> _logger;

    private readonly ContextDB _Db;

    public ChatRepositoryQueries(ContextDB db, ILogger<ChatRepositoriesCommands> logger)
    {
        _Db = db;
        _logger = logger;
    }
    public async Task<Chat> BuscarChat(Guid id)
    {
        var chat = await _Db.Chat.FirstOrDefaultAsync(x => x.ChatId == id);

        if (chat == null)
        {
            _logger.LogWarning($"Chat com ID {id} não encontrado.");
            throw new Exception($"Chat com ID {id} não encontrado.");
        }

        return chat;
    }

    public async Task<List<Chat>> BuscarChatsUsuario(Guid IdUser)
    {
        var chats = await _Db.Chat
            .Include(c => c.User)
            .Where(c => c.User.SendUser == IdUser || c.User.RecibeUser == IdUser)
            .ToListAsync();

        if (chats == null)
        {
            _logger.LogWarning($"Nenhum chat encontrado para o usuário com ID {IdUser}.");
            throw new Exception($"Usuario com o ID {IdUser} Não contem nenhum chat iniciado.");
        }
      
        _logger.LogInformation($"Foram encontrados {chats.Count} chats para o usuário com ID {IdUser}.");

        return chats;
        }
}
