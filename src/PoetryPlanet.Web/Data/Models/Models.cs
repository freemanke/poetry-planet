using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

namespace PoetryPlanet.Web.Data.Models
{
    [Table("authors")]
    public class Author
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }= "";

        [Column("intro")]
        public string Intro { get; set; }= "";

        [Column("quotes_count")]
        public int QuotesCount { get; set; }

        [Column("views_count")]
        public int ViewsCount { get; set; }

        [Column("dynasty")]
        public string Dynasty { get; set; }= "";

        [Column("birth_year")]
        public string BirthYear { get; set; }= "";

        [Column("death_year")]
        public string DeathYear { get; set; }= "";

        [Column("updated_at")]
        public string UpdatedAt { get; set; }= "";

        [Column("baidu_wiki")]
        public string BaiduWiki { get; set; }= "";

        [Column("remote_id")]
        public string RemoteId { get; set; }= "";

        [Column("works_count")]
        public int WorksCount { get; set; }

        [Column("works_shi_count")]
        public int WorksShiCount { get; set; }

        [Column("works_ci_count")]
        public int WorksCiCount { get; set; }

        [Column("works_wen_count")]
        public int WorksWenCount { get; set; }

        [Column("works_qu_count")]
        public int WorksQuCount { get; set; }

        [Column("works_fu_count")]
        public int WorksFuCount { get; set; }

        [Column("name_tr")]
        public string NameTr { get; set; }= "";

        [Column("intro_tr")]
        public string IntroTr { get; set; }= "";
    }
    
    [Table("dynasties")]
    public class Dynasty
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")] public string Name { get; set; } = "";

        [Column("intro")]
        public string Intro { get; set; }= "";

        [Column("start_year")]
        public int StartYear { get; set; }

        [Column("end_year")]
        public int EndYear { get; set; }

        [Column("name_tr")]
        public string NameTr { get; set; }= "";

        [Column("intro_tr")]
        public string IntroTr { get; set; }= "";
    }

    [Table("collection_kinds")]
    public class CollectionKind
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("show_order")]
        public int ShowOrder { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }= "";

        [Column("limit")]
        public int Limit { get; set; }

        [Column("name_tr")]
        public string NameTr { get; set; }= "";
    }

    [Table("collections")]
    public class Collection
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("show_order")]
        public int ShowOrder { get; set; }

        [Column("works_count")]
        public int WorksCount { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }= "";

        [Column("online_data")]
        public int OnlineData { get; set; }

        [Column("short_desc")]
        public string ShortDesc { get; set; }= "";

        [Column("desc")]
        public string Desc { get; set; }= "";

        [Column("cover")]
        public string Cover { get; set; }= "";

        [Column("kind_id")]
        public int KindId { get; set; }

        [Column("kind")] 
        public string Kind { get; set; } = "";

        [Column("quotes_count")]
        public int QuotesCount { get; set; }

        [Column("name_tr")]
        public string NameTr { get; set; }= "";

        [Column("short_desc_tr")]
        public string ShortDescTr { get; set; }= "";

        [Column("desc_tr")]
        public string DescTr { get; set; }= "";

        [Column("kind_tr")]
        public string KindTr { get; set; }= "";
    }

    [Table("collection_quotes")]
    public class CollectionQuote
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("show_order")]
        public int ShowOrder { get; set; }

        [Column("quote_id")]
        public int QuoteId { get; set; }

        [Column("quote")]
        public string Quote { get; set; }= "";

        [Column("quote_author")]
        public string QuoteAuthor { get; set; }= "";

        [Column("quote_work")]
        public string QuoteWork { get; set; }= "";

        [Column("quote_work_id")]
        public int QuoteWorkId { get; set; }

        [Column("collection_id")]
        public int CollectionId { get; set; }

        [Column("collection_kind_id")]
        public int CollectionKindId { get; set; }

        [Column("quote_tr")]
        public string QuoteTr { get; set; }= "";

        [Column("quote_author_tr")]
        public string QuoteAuthorTr { get; set; }= "";

        [Column("quote_work_tr")]
        public string QuoteWorkTr { get; set; }= "";
    }

    [Table("collection_works")]
    public class CollectionWork
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("show_order")]
        public int ShowOrder { get; set; }

        [Column("work_id")]
        public int WorkId { get; set; }

        [Column("work_title")]
        public string WorkTitle { get; set; }= "";

        [Column("work_full_title")]
        public string WorkFullTitle { get; set; }= "";

        [Column("work_author")]
        public string WorkAuthor { get; set; }= "";

        [Column("work_dynasty")]
        public string WorkDynasty { get; set; }= "";

        [Column("work_content")]
        public string WorkContent { get; set; }= "";

        [Column("work_kind")]
        public string WorkKind { get; set; }= "";

        [Column("collection_id")]
        public int CollectionId { get; set; }

        [Column("collection")]
        public string Collection { get; set; }= "";

        [Column("work_title_tr")]
        public string WorkTitleTr { get; set; }= "";

        [Column("work_full_title_tr")]
        public string WorkFullTitleTr { get; set; }= "";

        [Column("work_author_tr")]
        public string WorkAuthorTr { get; set; }= "";

        [Column("work_dynasty_tr")]
        public string WorkDynastyTr { get; set; }= "";

        [Column("work_content_tr")]
        public string WorkContentTr { get; set; }= "";

        [Column("collection_tr")]
        public string CollectionTr { get; set; }= "";
    }

    [Table("quotes")]
    public class Quote
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("quote")] public string QuoteText { get; set; } = "";

        [Column("dynasty")]
        public string Dynasty { get; set; }= "";

        [Column("author_id")]
        public int AuthorId { get; set; }

        [Column("author")]
        public string Author { get; set; }= "";

        [Column("kind")]
        public string Kind { get; set; }= "";

        [Column("work_id")]
        public int WorkId { get; set; }

        [Column("work_title")]
        public string WorkTitle { get; set; }= "";

        [Column("updated_at")]
        public string UpdatedAt { get; set; }= "";

        [Column("quote_tr")]
        public string QuoteTr { get; set; }= "";

        [Column("dynasty_tr")]
        public string DynastyTr { get; set; }= "";

        [Column("author_tr")]
        public string AuthorTr { get; set; }= "";

        [Column("work_title_tr")]
        public string WorkTitleTr { get; set; }= "";
    }

    [Table("works")]
    public class Work
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; }= "";

        public int ShowOrder { get; set; }

        [Column("posts_count")]
        public int PostsCount { get; set; }

        [Column("author_works_count")]
        public int AuthorWorksCount { get; set; }

        [Column("quotes_count")]
        public int QuotesCount { get; set; }

        [Column("collections_count")]
        public int CollectionsCount { get; set; }

        [Column("author")]
        public string Author { get; set; }= "";

        [Column("author_desc")]
        public string AuthorDesc { get; set; }= "";

        [Column("author_id")]
        public int AuthorId { get; set; }

        [Column("author_remote_id")]
        public string AuthorRemoteId { get; set; }= "";

        [Column("dynasty")]
        public string Dynasty { get; set; }= "";

        [Column("kind")]
        public string Kind { get; set; }= "";

        [Column("kind_cn")]
        public string KindCn { get; set; }= "";

        [Column("baidu_wiki")]
        public string BaiduWiki { get; set; }= "";

        [Column("foreword")]
        public string Foreword { get; set; }= "";

        [Column("content")]
        public string Content { get; set; }= "";

        [Column("intro")]
        public string Intro { get; set; }= "";

        [Column("annotation")]
        public string Annotation { get; set; }= "";

        [Column("translation")]
        public string Translation { get; set; }= "";

        [Column("master_comment")]
        public string MasterComment { get; set; }= "";

        [Column("layout")]
        public string Layout { get; set; }= "";

        [Column("highlighted_at")]
        public int HighlightedAt { get; set; }

        [Column("title_tr")]
        public string TitleTr { get; set; }= "";

        [Column("author_tr")]
        public string AuthorTr { get; set; }= "";

        [Column("author_desc_tr")]
        public string AuthorDescTr { get; set; }= "";

        [Column("dynasty_tr")]
        public string DynastyTr { get; set; }= "";

        [Column("kind_cn_tr")]
        public string KindCnTr { get; set; }= "";

        [Column("foreword_tr")]
        public string ForewordTr { get; set; }= "";

        [Column("content_tr")]
        public string ContentTr { get; set; }= "";

        [Column("intro_tr")]
        public string IntroTr { get; set; }= "";

        [Column("annotation_tr")]
        public string AnnotationTr { get; set; }= "";

        [Column("translation_tr")]
        public string TranslationTr { get; set; }= "";

        [Column("master_comment_tr")]
        public string MasterCommentTr { get; set; }= "";
    }
}