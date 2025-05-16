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

public class CollectionKindInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("show_order")]
    public int ShowOrder { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("name_tr")]
    public string? NameTr { get; set; }
}

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