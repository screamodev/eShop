using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        await context.Database.EnsureCreatedAsync();

        if (!context.CatalogSizes.Any())
        {
            await context.CatalogSizes.AddRangeAsync(GetPreconfiguredCatalogSizes());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogGenders.Any())
        {
            await context.CatalogGenders.AddRangeAsync(GetPreconfiguredCatalogGenders());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogBrands.Any())
        {
            await context.CatalogBrands.AddRangeAsync(GetPreconfiguredCatalogBrands());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogTypes.Any())
        {
            await context.CatalogTypes.AddRangeAsync(GetPreconfiguredCatalogTypes());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogItems.Any())
        {
            var sizes = context.CatalogSizes.ToList();

            await context.CatalogItems.AddRangeAsync(GetPreconfiguredItems(sizes));

            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<CatalogSize> GetPreconfiguredCatalogSizes()
    {
        return new List<CatalogSize>()
        {
            new CatalogSize() { Size = "S" },
            new CatalogSize() { Size = "M" },
            new CatalogSize() { Size = "L" },
        };
    }

    private static IEnumerable<CatalogGender> GetPreconfiguredCatalogGenders()
    {
        return new List<CatalogGender>()
        {
            new CatalogGender() { Gender = "Men" },
            new CatalogGender() { Gender = "Woman" },
        };
    }

    private static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
    {
        return new List<CatalogBrand>()
        {
            new CatalogBrand() { Brand = "Nebula" },
            new CatalogBrand() { Brand = "Exorbita" },
            new CatalogBrand() { Brand = "AuralElite" },
        };
    }

    private static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogType>()
        {
            new CatalogType() { Type = "Electronics" },
            new CatalogType() { Type = "Clothes" },
            new CatalogType() { Type = "Furniture" },
        };
    }

    private static IEnumerable<CatalogItem> GetPreconfiguredItems(List<CatalogSize> catalogSizes)
    {
        return new List<CatalogItem>()
        {
            new CatalogItem { CatalogItemSizes = catalogSizes.Where(i => i.Id == 1).ToList(), CatalogGenderId = 1, CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 10, Description = "Immerse yourself in cosmic fashion. Unveil the enigmatic allure of the Nebula Noir Hoodie. Embrace its cozy and durable charm. Elevate your style to celestial heights. Get yours today!", Name = "Noir Hoodie", Price = 199.00m, PictureFileName = "1.png" },
            new CatalogItem { CatalogItemSizes = catalogSizes.Where(i => i.Id == 2).ToList(), CatalogGenderId = 1, CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = "Introducing the Exorbita Elegance Elite watch, now available with the option of kinetic movement technology. Immerse yourself in timeless elegance and never worry about battery changes again. Discover the perfect blend of style and innovation.", Name = "Elegance Elite", Price = 1199.00m, PictureFileName = "2.jpg" },
            new CatalogItem { CatalogItemSizes = catalogSizes.Where(i => i.Id == 3).ToList(), CatalogGenderId = 1, CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = "Elevate your travel experience with the luxurious Pinnacle Posh Pack. Crafted from genuine leather, this stylish backpack is tailor-made for modern adventurers. It's handmade, durable, and exudes a touch of sophistication. Upgrade your travel game today!", Name = "Posh Pack", Price = 405.00m, PictureFileName = "3.jpg" },
            new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 3, AvailableStock = 100, Description = "Introducing the Corporate Commando Throne office chair. Experience the ultimate in ergonomic comfort and stylish design. Enhance productivity while commanding authority. Get the Corporate Commando Throne today!", Name = "Corporate Commando Throne", Price = 550.00m, PictureFileName = "4.jpg" },
            new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 3, AvailableStock = 100, Description = "Exquisite steel design lamp. Sleek construction exudes elegance and modernity. Illuminate with style and sophistication. Elevate your decor effortlessly. Perfect for warm and inviting ambiance. Unleash your creativity. Experience steel in a new light.", Name = "Metallic Majesty Illuminator", Price = 399.00m, PictureFileName = "5.jpg" },
            new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 1, AvailableStock = 100, Description = "Because ordinary blenders are for the common folk. With the BlendMaster, you can effortlessly mix your pretentious smoothies and soups while feeling like a culinary genius. It's not just a blender; it's a status symbol in the world of haute cuisine.", Name = "BlendMaster Elite Fusionator", Price = 199.00m, PictureFileName = "6.jpg" },
            new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 3, AvailableStock = 100, Description = "Introducing the mighty Decibel Dominator Deluxe clock radio alarm! Experience its seamless Bluetooth connectivity and crystal-clear DAB+ radio. Rise and shine in style!", Name = "Decibel Dominator Deluxe", Price = 199.00m, PictureFileName = "7.jpg" },
            new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 1, AvailableStock = 100, Description = "Immerse in audio with the Audio Arrogance AuralElite Bluetooth headphones. Enjoy Active Noise Cancellation (ANC) for immersive experience. Indulge in flawless sound.", Name = "Audio Arrogance AuralElite", Price = 220.00m, PictureFileName = "8.jpg" },
            new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 2, AvailableStock = 100, Description = "Immerse in authentic sound and timeless charm with the Vinyl Virtuoso Opulenza. Elevate your listening experience with this vintage-inspired Analog Turntable. Rediscover music's essence now.", Name = "Vinyl Virtuoso Opulenza", Price = 699.00m, PictureFileName = "9.jpg" },
        };
    }
}