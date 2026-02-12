using Api.Features.Purchase.Models.Enums;

namespace Api.Extensions;

public static class ProductTypeExtensions
{
    public static string ToFriendlyName(this ProductType type)
    {
        return type switch
        {
            ProductType.ShortSleeve => "Remera Manga Corta",
            ProductType.LongSleeve => "Remera Manga Larga",
            ProductType.Sleeveless => "Musculosa",
            ProductType.Polo => "Chomba",
            _ => type.ToString()
        };
    }
}