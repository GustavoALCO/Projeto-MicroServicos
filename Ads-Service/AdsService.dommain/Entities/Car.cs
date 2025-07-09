using AdsService.Dommain.Enums;

namespace AdsService.Dommain.Entities;

public class Car
{
    //Categorias para os carros
    public CarBrand? CarBrand { get; set; }

    public string? Model { get; set; }

    public string? Type { get; set; }

    public string? Year { get; set; }

    public int? KM { get; set; }

    public decimal? Engine { get; set; }

    public Fuel? Fuel { get; set; }

    public bool? Gearbox { get; set; }

    public Color? Color { get; set; }
}
