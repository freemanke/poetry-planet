using System.Text.Json.Serialization;

namespace PoetryPlanet.Dtos;

public class CollectionInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("show_order")]
    public int ShowOrder { get; set; }

    [JsonPropertyName("works_count")]
    public int WorksCount { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("online_data")]
    public int OnlineData { get; set; }

    [JsonPropertyName("short_desc")]
    public string? ShortDesc { get; set; }

    [JsonPropertyName("desc")]
    public string? Desc { get; set; }

    [JsonPropertyName("cover")]
    public string? Cover { get; set; }

    [JsonPropertyName("kind_id")]
    public int KindId { get; set; }

    [JsonPropertyName("kind")] 
    public string? Kind { get; set; } 

    [JsonPropertyName("quotes_count")]
    public int QuotesCount { get; set; }

    [JsonPropertyName("name_tr")]
    public string? NameTr { get; set; }

    [JsonPropertyName("short_desc_tr")]
    public string? ShortDescTr { get; set; }

    [JsonPropertyName("desc_tr")]
    public string? DescTr { get; set; }

    [JsonPropertyName("kind_tr")]
    public string? KindTr { get; set; }
}