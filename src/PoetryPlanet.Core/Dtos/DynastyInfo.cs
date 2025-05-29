using System.Text.Json.Serialization;

namespace PoetryPlanet.Dtos;

public class DynastyInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; } 

    [JsonPropertyName("intro")]
    public string? Intro { get; set; }

    [JsonPropertyName("start_year")]
    public int StartYear { get; set; }

    [JsonPropertyName("end_year")]
    public int EndYear { get; set; }

    [JsonPropertyName("name_tr")]
    public string? NameTr { get; set; }

    [JsonPropertyName("intro_tr")]
    public string? IntroTr { get; set; }
}