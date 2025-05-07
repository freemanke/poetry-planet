
using Newtonsoft.Json;

namespace PoetryPlanet.Dtos;

public class AuthorList
{
    [JsonProperty("authors")]
    public List<AuthorInfo> Items { get; set; } = [];
}

public class CollectionKindList
{
    [JsonProperty("collection_kinds")]
    public List<CollectionKindInfo> Items { get; set; } = [];
}

public class CollectionQuoteList
{
    [JsonProperty("collection_quotes")]
    public List<CollectionQuoteInfo> Items { get; set; } = [];
}

public class CollectionWorkList
{
    [JsonProperty("collection_works")]
    public List<CollectionWorkInfo> Items { get; set; } = [];
}

public class CollectionList
{
    [JsonProperty("collections")]
    public List<CollectionInfo> Items { get; set; } = [];
}

public class DynastyList
{
    [JsonProperty("dynasties")]
    public List<DynastyInfo> Items { get; set; } = [];
}

public class QuoteList
{
    [JsonProperty("quotes")]
    public List<QuoteInfo> Items { get; set; } = [];
}

public class WorkList
{
    [JsonProperty("works")]
    public List<WorkInfo> Items { get; set; } = [];
}