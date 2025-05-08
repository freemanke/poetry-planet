using System.Text.Json.Serialization;

namespace PoetryPlanet.Dtos;

public class WorkInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("show_order")]
    public int ShowOrder { get; set; }

    [JsonPropertyName("posts_count")]
    public int PostsCount { get; set; }

    [JsonPropertyName("author_works_count")]
    public int AuthorWorksCount { get; set; }

    [JsonPropertyName("quotes_count")]
    public int QuotesCount { get; set; }

    [JsonPropertyName("collections_count")]
    public int CollectionsCount { get; set; }

    [JsonPropertyName("author")]
    public string? Author { get; set; }

    [JsonPropertyName("author_desc")]
    public string? AuthorDesc { get; set; }

    [JsonPropertyName("author_id")]
    public int AuthorId { get; set; }

    [JsonPropertyName("author_remote_id")]
    public string? AuthorRemoteId { get; set; }

    [JsonPropertyName("dynasty")]
    public string? Dynasty { get; set; }

    [JsonPropertyName("kind")]
    public string? Kind { get; set; }

    [JsonPropertyName("kind_cn")]
    public string? KindCn { get; set; }

    [JsonPropertyName("baidu_wiki")]
    public string? BaiduWiki { get; set; }

    [JsonPropertyName("foreword")]
    public string? Foreword { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("intro")]
    public string? Intro { get; set; }

    [JsonPropertyName("annotation")]
    public string? Annotation { get; set; }

    [JsonPropertyName("translation")]
    public string? Translation { get; set; }

    [JsonPropertyName("master_comment")]
    public string? MasterComment { get; set; }

    [JsonPropertyName("layout")]
    public string? Layout { get; set; }

    [JsonPropertyName("highlighted_at")]
    public int HighlightedAt { get; set; }

    [JsonPropertyName("title_tr")]
    public string? TitleTr { get; set; }

    [JsonPropertyName("author_tr")]
    public string? AuthorTr { get; set; }

    [JsonPropertyName("author_desc_tr")]
    public string? AuthorDescTr { get; set; }

    [JsonPropertyName("dynasty_tr")]
    public string? DynastyTr { get; set; }

    [JsonPropertyName("kind_cn_tr")]
    public string? KindCnTr { get; set; }

    [JsonPropertyName("foreword_tr")]
    public string? ForewordTr { get; set; }

    [JsonPropertyName("content_tr")]
    public string? ContentTr { get; set; }

    [JsonPropertyName("intro_tr")]
    public string? IntroTr { get; set; }

    [JsonPropertyName("annotation_tr")]
    public string? AnnotationTr { get; set; }

    [JsonPropertyName("translation_tr")]
    public string? TranslationTr { get; set; }

    [JsonPropertyName("master_comment_tr")]
    public string? MasterCommentTr { get; set; }
}