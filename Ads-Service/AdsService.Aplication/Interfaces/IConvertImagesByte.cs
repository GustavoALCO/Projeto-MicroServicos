namespace AdsService.Aplication.Interfaces;

public interface IConvertImagesByte
{
    public byte[] ConvertImageToByteAsync(string imagePath);
}
