using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoetryPlanet.Data.Models;

[Table("collections")]
public class Collection
{
    public Collection()
    {
    }

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