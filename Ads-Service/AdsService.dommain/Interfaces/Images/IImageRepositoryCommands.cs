namespace AdsService.Dommain.Interfaces.Images;

public interface IImageRepositoryCommands
{
    Task CreateAsync(Dommain.Entities.Images image); // Cria uma nova imagem no banco de dados
    Task UpdateAsync(Dommain.Entities.Images image); // Atualiza uma imagem existente no banco de dados
    Task DeleteAsync(Dommain.Entities.Images image); // Exclui uma imagem do banco de dados
}
