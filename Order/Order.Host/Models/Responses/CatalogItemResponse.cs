using System.Text.Json.Serialization;

namespace Order.Host.Models.Responses;

public class CatalogItemResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("pictureUrl")]
    public string PictureFileName { get; set; }

    [JsonPropertyName("availableStock")]
    public int AvailableStock { get; set; }
}