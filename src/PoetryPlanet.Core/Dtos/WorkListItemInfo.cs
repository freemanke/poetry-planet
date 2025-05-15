using System.Text.Json.Serialization;

namespace PoetryPlanet.Dtos;

public class WorkListItemInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("author")]
    public string? Author { get; set; }

    [JsonPropertyName("author_id")]
    public int AuthorId { get; set; }
    
    [JsonPropertyName("dynasty")]
    public string? Dynasty { get; set; }
    
    [JsonPropertyName("content")]
    public string? Content { get; set; }
}