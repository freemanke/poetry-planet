using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

namespace PoetryPlanet.Web.Data.ModelDtos
{
    [Table("authors")]
    public class Author
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }= "";

        [JsonPropertyName("intro")]
        public string Intro { get; set; }= "";

        [JsonPropertyName("quotes_count")]
        public int QuotesCount { get; set; }

        [JsonPropertyName("views_count")]
        public int ViewsCount { get; set; }

        [JsonPropertyName("dynasty")]
        public string Dynasty { get; set; }= "";

        [JsonPropertyName("birth_year")]
        public string BirthYear { get; set; }= "";

        [JsonPropertyName("death_year")]
        public string DeathYear { get; set; }= "";

        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set; }= "";

        [JsonPropertyName("baidu_wiki")]
        public string BaiduWiki { get; set; }= "";

        [JsonPropertyName("remote_id")]
        public string RemoteId { get; set; }= "";

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
        public string NameTr { get; set; }= "";

        [JsonPropertyName("intro_tr")]
        public string IntroTr { get; set; }= "";
    }
    
    [Table("dynasties")]
    public class Dynasty
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; } = "";

        [JsonPropertyName("intro")]
        public string Intro { get; set; }= "";

        [JsonPropertyName("start_year")]
        public int StartYear { get; set; }

        [JsonPropertyName("end_year")]
        public int EndYear { get; set; }

        [JsonPropertyName("name_tr")]
        public string NameTr { get; set; }= "";

        [JsonPropertyName("intro_tr")]
        public string IntroTr { get; set; }= "";
    }

    [Table("collection_kinds")]
    public class CollectionKind
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("show_order")]
        public int ShowOrder { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }= "";

        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        [JsonPropertyName("name_tr")]
        public string NameTr { get; set; }= "";
    }

    [Table("collections")]
    public class Collection
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("show_order")]
        public int ShowOrder { get; set; }

        [JsonPropertyName("works_count")]
        public int WorksCount { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }= "";

        [JsonPropertyName("online_data")]
        public int OnlineData { get; set; }

        [JsonPropertyName("short_desc")]
        public string ShortDesc { get; set; }= "";

        [JsonPropertyName("desc")]
        public string Desc { get; set; }= "";

        [JsonPropertyName("cover")]
        public string Cover { get; set; }= "";

        [JsonPropertyName("kind_id")]
        public int KindId { get; set; }

        [JsonPropertyName("kind")] 
        public string Kind { get; set; } = "";

        [JsonPropertyName("quotes_count")]
        public int QuotesCount { get; set; }

        [JsonPropertyName("name_tr")]
        public string NameTr { get; set; }= "";

        [JsonPropertyName("short_desc_tr")]
        public string ShortDescTr { get; set; }= "";

        [JsonPropertyName("desc_tr")]
        public string DescTr { get; set; }= "";

        [JsonPropertyName("kind_tr")]
        public string KindTr { get; set; }= "";
    }

    [Table("collection_quotes")]
    public class CollectionQuote
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("show_order")]
        public int ShowOrder { get; set; }

        [JsonPropertyName("quote_id")]
        public int QuoteId { get; set; }

        [JsonPropertyName("quote")]
        public string Quote { get; set; }= "";

        [JsonPropertyName("quote_author")]
        public string QuoteAuthor { get; set; }= "";

        [JsonPropertyName("quote_work")]
        public string QuoteWork { get; set; }= "";

        [JsonPropertyName("quote_work_id")]
        public int QuoteWorkId { get; set; }

        [JsonPropertyName("collection_id")]
        public int CollectionId { get; set; }

        [JsonPropertyName("collection_kind_id")]
        public int CollectionKindId { get; set; }

        [JsonPropertyName("quote_tr")]
        public string QuoteTr { get; set; }= "";

        [JsonPropertyName("quote_author_tr")]
        public string QuoteAuthorTr { get; set; }= "";

        [JsonPropertyName("quote_work_tr")]
        public string QuoteWorkTr { get; set; }= "";
    }

    [Table("collection_works")]
    public class CollectionWork
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("show_order")]
        public int ShowOrder { get; set; }

        [JsonPropertyName("work_id")]
        public int WorkId { get; set; }

        [JsonPropertyName("work_title")]
        public string WorkTitle { get; set; }= "";

        [JsonPropertyName("work_full_title")]
        public string WorkFullTitle { get; set; }= "";

        [JsonPropertyName("work_author")]
        public string WorkAuthor { get; set; }= "";

        [JsonPropertyName("work_dynasty")]
        public string WorkDynasty { get; set; }= "";

        [JsonPropertyName("work_content")]
        public string WorkContent { get; set; }= "";

        [JsonPropertyName("work_kind")]
        public string WorkKind { get; set; }= "";

        [JsonPropertyName("collection_id")]
        public int CollectionId { get; set; }

        [JsonPropertyName("collection")]
        public string Collection { get; set; }= "";

        [JsonPropertyName("work_title_tr")]
        public string WorkTitleTr { get; set; }= "";

        [JsonPropertyName("work_full_title_tr")]
        public string WorkFullTitleTr { get; set; }= "";

        [JsonPropertyName("work_author_tr")]
        public string WorkAuthorTr { get; set; }= "";

        [JsonPropertyName("work_dynasty_tr")]
        public string WorkDynastyTr { get; set; }= "";

        [JsonPropertyName("work_content_tr")]
        public string WorkContentTr { get; set; }= "";

        [JsonPropertyName("collection_tr")]
        public string CollectionTr { get; set; }= "";
    }

    [Table("quotes")]
    public class Quote
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("quote")] public string QuoteText { get; set; } = "";

        [JsonPropertyName("dynasty")]
        public string Dynasty { get; set; }= "";

        [JsonPropertyName("author_id")]
        public int AuthorId { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }= "";

        [JsonPropertyName("kind")]
        public string Kind { get; set; }= "";

        [JsonPropertyName("work_id")]
        public int WorkId { get; set; }

        [JsonPropertyName("work_title")]
        public string WorkTitle { get; set; }= "";

        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set; }= "";

        [JsonPropertyName("quote_tr")]
        public string QuoteTr { get; set; }= "";

        [JsonPropertyName("dynasty_tr")]
        public string DynastyTr { get; set; }= "";

        [JsonPropertyName("author_tr")]
        public string AuthorTr { get; set; }= "";

        [JsonPropertyName("work_title_tr")]
        public string WorkTitleTr { get; set; }= "";
    }

    [Table("works")]
    public class Work
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("title")]
        public string Title { get; set; }= "";

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
        public string Author { get; set; }= "";

        [JsonPropertyName("author_desc")]
        public string AuthorDesc { get; set; }= "";

        [JsonPropertyName("author_id")]
        public int AuthorId { get; set; }

        [JsonPropertyName("author_remote_id")]
        public string AuthorRemoteId { get; set; }= "";

        [JsonPropertyName("dynasty")]
        public string Dynasty { get; set; }= "";

        [JsonPropertyName("kind")]
        public string Kind { get; set; }= "";

        [JsonPropertyName("kind_cn")]
        public string KindCn { get; set; }= "";

        [JsonPropertyName("baidu_wiki")]
        public string BaiduWiki { get; set; }= "";

        [JsonPropertyName("foreword")]
        public string Foreword { get; set; }= "";

        [JsonPropertyName("content")]
        public string Content { get; set; }= "";

        [JsonPropertyName("intro")]
        public string Intro { get; set; }= "";

        [JsonPropertyName("annotation")]
        public string Annotation { get; set; }= "";

        [JsonPropertyName("translation")]
        public string Translation { get; set; }= "";

        [JsonPropertyName("master_comment")]
        public string MasterComment { get; set; }= "";

        [JsonPropertyName("layout")]
        public string Layout { get; set; }= "";

        [JsonPropertyName("highlighted_at")]
        public int HighlightedAt { get; set; }

        [JsonPropertyName("title_tr")]
        public string TitleTr { get; set; }= "";

        [JsonPropertyName("author_tr")]
        public string AuthorTr { get; set; }= "";

        [JsonPropertyName("author_desc_tr")]
        public string AuthorDescTr { get; set; }= "";

        [JsonPropertyName("dynasty_tr")]
        public string DynastyTr { get; set; }= "";

        [JsonPropertyName("kind_cn_tr")]
        public string KindCnTr { get; set; }= "";

        [JsonPropertyName("foreword_tr")]
        public string ForewordTr { get; set; }= "";

        [JsonPropertyName("content_tr")]
        public string ContentTr { get; set; }= "";

        [JsonPropertyName("intro_tr")]
        public string IntroTr { get; set; }= "";

        [JsonPropertyName("annotation_tr")]
        public string AnnotationTr { get; set; }= "";

        [JsonPropertyName("translation_tr")]
        public string TranslationTr { get; set; }= "";

        [JsonPropertyName("master_comment_tr")]
        public string MasterCommentTr { get; set; }= "";
    }
}