namespace AdsService.Aplication.Settings;

public class ConnectionString
{
    public required int Port { get; set; } 
    public required string Database { get; set; } 
    public required string Username { get; set; }
    public required string Password { get; set; }
}
