using AdsService.Dommain.Enums;

namespace AdsService.Dommain.Entities;

public class Category
{
    public required CategoryType CategoryType { get; set; }

    public Car? Car { get; set; }

    public Houses? House { get; set; }
}