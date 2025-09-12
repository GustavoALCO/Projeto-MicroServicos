using AuthUsers.infra.DbConfig;
using ChatService.dommain.Entities;
using ChatService.dommain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ChatService.infra.Repositories.ChatCommands;

public class ChatRepositoriesCommands : IChatRepositoryCommands
{

    private readonly ILogger<ChatRepositoriesCommands> _logger;

    private readonly ContextDB _Db;

    public ChatRepositoriesCommands(ContextDB db, ILogger<ChatRepositoriesCommands> logger)
    {
        _Db = db;
        _logger = logger;
    }

    public async Task CreateChat(Chat chat)
    {
       await _Db.AddAsync(chat);

       await _Db.SaveChangesAsync();

       _logger.LogInformation($"Chat Criado Por {chat.User.SendUser}. Sendo Enviado para o {chat.User.RecibeUser}");
    }

    public async Task UpdateChat(Chat chat)
    {
         _Db.Update(chat);

        await _Db.SaveChangesAsync();
    }

}
