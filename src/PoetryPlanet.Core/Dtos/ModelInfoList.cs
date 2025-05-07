using System.Text.Json.Serialization;

namespace PoetryPlanet.Dtos;

public class AuthorList
{
    [JsonPropertyName("authors")]
    public List<AuthorInfo> Items { get; set; } = [];
}

public class CollectionKindList
{
    [JsonPropertyName("collection_kinds")]
    public List<CollectionKindInfo> Items { get; set; } = [];
}

public class CollectionQuoteList
{
    [JsonPropertyName("collection_quotes")]
    public List<CollectionQuoteInfo> Items { get; set; } = [];
}

public class CollectionWorkList
{
    [JsonPropertyName("collection_works")]
    public List<CollectionWorkInfo> Items { get; set; } = [];
}

public class CollectionList
{
    [JsonPropertyName("collections")]
    public List<CollectionInfo> Items { get; set; } = [];
}

public class DynastyList
{
    [JsonPropertyName("dynasties")]
    public List<DynastyInfo> Items { get; set; } = [];
}

public class QuoteList
{
    [JsonPropertyName("quotes")]
    public List<QuoteInfo> Items { get; set; } = [];
}

public class WorkList
{
    [JsonPropertyName("works")]
    public List<WorkInfo> Items { get; set; } = [];
}