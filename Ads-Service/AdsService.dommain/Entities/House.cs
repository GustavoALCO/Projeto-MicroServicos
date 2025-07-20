using AdsService.Dommain.Enums;

namespace AdsService.Dommain.Entities;

public class Houses
{
    //Categorias para uma Casa
    public House? House { get; set; }

    public int SquareMeters { get; set; }

    public int? Bedroom { get; set; }

    public int? Bathroom { get; set; }

    public int CarSpace { get; set; }
}
