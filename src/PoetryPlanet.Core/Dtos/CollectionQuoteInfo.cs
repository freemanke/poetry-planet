using System.Text.Json.Serialization;

namespace PoetryPlanet.Dtos;

public class CollectionQuoteInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("show_order")]
    public int ShowOrder { get; set; }

    [JsonPropertyName("quote_id")]
    public int QuoteId { get; set; }

    [JsonPropertyName("quote")]
    public string? Quote { get; set; }

    [JsonPropertyName("quote_author")]
    public string? QuoteAuthor { get; set; }

    [JsonPropertyName("quote_work")]
    public string? QuoteWork { get; set; }

    [JsonPropertyName("quote_work_id")]
    public int QuoteWorkId { get; set; }

    [JsonPropertyName("collection_id")]
    public int CollectionId { get; set; }

    [JsonPropertyName("collection_kind_id")]
    public int CollectionKindId { get; set; }

    [JsonPropertyName("quote_tr")]
    public string? QuoteTr { get; set; }

    [JsonPropertyName("quote_author_tr")]
    public string? QuoteAuthorTr { get; set; }

    [JsonPropertyName("quote_work_tr")]
    public string? QuoteWorkTr { get; set; }
}