namespace AdsService.Dommain.Entities;

public class Images
{
    //public string FileName { get; set; } = string.Empty;
    //public string ContentType { get; set; } = string.Empty; // Ex: image/png
    public required byte[] Data { get; set; } // Aqui vai a imagem binária
}
