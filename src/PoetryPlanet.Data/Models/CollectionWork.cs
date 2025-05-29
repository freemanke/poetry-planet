using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoetryPlanet.Data.Models;

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