using AdsService.Aplication.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdsService.Aplication.Services;

public class ConvertImagesByte : IConvertImagesByte
{
    private readonly ILogger<ConvertImagesByte> _logger;

    public ConvertImagesByte(ILogger<ConvertImagesByte> logger)
    {
        _logger = logger;
    }

    public byte[] ConvertImageToByteAsync(string imagePath)
    {
        //Verefica onde esta o index da virgula na string base64 recebida
        var commaIndex = imagePath.IndexOf(',');

        // Se a virgula for encontrada, remove tudo que estiver antes dela
        if (commaIndex >= 0)
            imagePath = imagePath[(commaIndex + 1)..];

        // Converte a string base64 em um array de bytes
        byte[] bytesImagem = Convert.FromBase64String(imagePath);

        _logger.LogInformation("Imagem convertida com sucesso para bytes. Tamanho: {Size} bytes", bytesImagem.Length);

        // retorna o array de bytes para listar na list futuralmente
        return bytesImagem;
    }
}
