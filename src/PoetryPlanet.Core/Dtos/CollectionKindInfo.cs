using System.Text.Json.Serialization;

namespace PoetryPlanet.Dtos;

public class CollectionKindInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("show_order")]
    public int ShowOrder { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("name_tr")]
    public string? NameTr { get; set; }
}