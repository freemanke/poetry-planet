using System.Text.Json.Serialization;

namespace PoetryPlanet.Dtos;

public class AuthorInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("intro")]
    public string? Intro { get; set; }

    [JsonPropertyName("quotes_count")]
    public int QuotesCount { get; set; }

    [JsonPropertyName("views_count")]
    public int ViewsCount { get; set; }

    [JsonPropertyName("dynasty")]
    public string? Dynasty { get; set; }

    [JsonPropertyName("birth_year")]
    public string? BirthYear { get; set; }

    [JsonPropertyName("death_year")]
    public string? DeathYear { get; set; }

    [JsonPropertyName("updated_at")]
    public string? UpdatedAt { get; set; }

    [JsonPropertyName("baidu_wiki")]
    public string? BaiduWiki { get; set; }

    [JsonPropertyName("remote_id")]
    public string? RemoteId { get; set; }

    [JsonPropertyName("works_count")]
    public int WorksCount { get; set; }

    [JsonPropertyName("works_shi_count")]
    public int WorksShiCount { get; set; }

    [JsonPropertyName("works_ci_count")]
    public int WorksCiCount { get; set; }

    [JsonPropertyName("works_wen_count")]
    public int WorksWenCount { get; set; }

    [JsonPropertyName("works_qu_count")]
    public int WorksQuCount { get; set; }

    [JsonPropertyName("works_fu_count")]
    public int WorksFuCount { get; set; }

    [JsonPropertyName("name_tr")]
    public string? NameTr { get; set; }

    [JsonPropertyName("intro_tr")]
    public string? IntroTr { get; set; }
}