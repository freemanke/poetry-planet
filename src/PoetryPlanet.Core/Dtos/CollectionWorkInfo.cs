using System.Text.Json.Serialization;

namespace PoetryPlanet.Dtos;

public class CollectionWorkInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("show_order")]
    public int ShowOrder { get; set; }

    [JsonPropertyName("work_id")]
    public int WorkId { get; set; }

    [JsonPropertyName("work_title")]
    public string? WorkTitle { get; set; }

    [JsonPropertyName("work_full_title")]
    public string? WorkFullTitle { get; set; }

    [JsonPropertyName("work_author")]
    public string? WorkAuthor { get; set; }

    [JsonPropertyName("work_dynasty")]
    public string? WorkDynasty { get; set; }

    [JsonPropertyName("work_content")]
    public string? WorkContent { get; set; }

    [JsonPropertyName("work_kind")]
    public string? WorkKind { get; set; }

    [JsonPropertyName("collection_id")]
    public int CollectionId { get; set; }

    [JsonPropertyName("collection")]
    public string? Collection { get; set; }

    [JsonPropertyName("work_title_tr")]
    public string? WorkTitleTr { get; set; }

    [JsonPropertyName("work_full_title_tr")]
    public string? WorkFullTitleTr { get; set; }

    [JsonPropertyName("work_author_tr")]
    public string? WorkAuthorTr { get; set; }

    [JsonPropertyName("work_dynasty_tr")]
    public string? WorkDynastyTr { get; set; }

    [JsonPropertyName("work_content_tr")]
    public string? WorkContentTr { get; set; }

    [JsonPropertyName("collection_tr")]
    public string? CollectionTr { get; set; }
}