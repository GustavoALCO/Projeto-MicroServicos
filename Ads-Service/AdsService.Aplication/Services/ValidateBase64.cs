using AdsService.Aplication.Interfaces;
using Microsoft.Extensions.Logging;

namespace AdsService.Aplication.Services;

public class ValidateBase64 : IValidateBase64
{
    private readonly ILogger<ValidateBase64> _logger;

    public ValidateBase64(ILogger<ValidateBase64> logger)
    {
        _logger = logger;
    }

    public bool IsValidBase64String(List<string> base64String)
    {
        // Verifica se a lista de strings Base64 é nula ou vazia
        if (base64String == null || base64String.Count == 0)
        {
            _logger.LogCritical("A lista de strings Base64 está vazia ou nula.");
            return false;
        }

        // Verifica se cada string Base64 na lista é válida
        foreach (string Base64 in base64String) 
            {

            //Remove os espacos que conter na string
           string base64 = Base64.Trim();

            //Verifica onde esta o index da virgula na string base64 recebida
            var commaIndex = Base64.IndexOf(',');

            // Se a virgula for encontrada, remove tudo que estiver antes dela
            if (commaIndex >= 0)
                 base64 = Base64[(commaIndex + 1)..];


                // Verifica se a string é multipla de 4, pois Base64 deve ter comprimento múltiplo de 4
                if (base64.Length % 4 != 0)
                {
                    _logger.LogCritical("A string Base64 não é válida: {Base64}", base64);
                    return false;
                }
                    
                // Verifica se a string contém apenas caracteres válidos de Base64
                foreach (char c in base64)
                {
                    // Verifica se o caractere é um dígito, letra, '+' ou '/' ou '='
                    if (!char.IsLetterOrDigit(c) && c != '+' && c != '/' && c != '=')
                    {
                        _logger.LogCritical("A string Base64 contém caracteres inválidos: {Base64}", base64);
                    return false;
                    }  
                }
            }

        return true;
    }
        
}


