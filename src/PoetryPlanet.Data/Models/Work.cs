using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoetryPlanet.Data.Models;

[Table("works")]
public class Work
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("title")]
    public string Title { get; set; } = "";

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