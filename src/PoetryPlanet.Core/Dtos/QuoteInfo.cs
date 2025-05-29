using System.Text.Json.Serialization;

namespace PoetryPlanet.Dtos;

public class QuoteInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("quote")] 
    public string? QuoteText { get; set; } 

    [JsonPropertyName("dynasty")]
    public string? Dynasty { get; set; }

    [JsonPropertyName("author_id")]
    public int AuthorId { get; set; }

    [JsonPropertyName("author")]
    public string? Author { get; set; }

    [JsonPropertyName("kind")]
    public string? Kind { get; set; }

    [JsonPropertyName("work_id")]
    public int WorkId { get; set; }

    [JsonPropertyName("work_title")]
    public string? WorkTitle { get; set; }

    [JsonPropertyName("updated_at")]
    public string? UpdatedAt { get; set; }

    [JsonPropertyName("quote_tr")]
    public string? QuoteTr { get; set; }

    [JsonPropertyName("dynasty_tr")]
    public string? DynastyTr { get; set; }

    [JsonPropertyName("author_tr")]
    public string? AuthorTr { get; set; }

    [JsonPropertyName("work_title_tr")]
    public string? WorkTitleTr { get; set; }
}