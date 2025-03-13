using System.Text.Json.Serialization;

namespace PoetryPlanet.Web.Data.ModelDtos;

public class AuthorList
{
    [JsonPropertyName("authors")]
    public List<Author> Items { get; set; } = [];
}

public class CollectionKindList
{
    [JsonPropertyName("collection_kinds")]
    public List<CollectionKind> Items { get; set; } = [];
}

public class CollectionQuoteList
{
    [JsonPropertyName("collection_quotes")]
    public List<CollectionQuote> Items { get; set; } = [];
}

public class CollectionWorkList
{
    [JsonPropertyName("collection_works")]
    public List<CollectionWork> Items { get; set; } = [];
}

public class CollectionList
{
    [JsonPropertyName("collections")]
    public List<Collection> Items { get; set; } = [];
}

public class DynastyList
{
    [JsonPropertyName("dynasties")]
    public List<Dynasty> Items { get; set; } = [];
}

public class QuoteList
{
    [JsonPropertyName("quotes")]
    public List<Quote> Items { get; set; } = [];
}

public class WorkList
{
    [JsonPropertyName("works")]
    public List<Work> Items { get; set; } = [];
}