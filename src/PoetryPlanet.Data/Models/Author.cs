using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoetryPlanet.Data.Models;

[Table("authors")]
public class Author
{
    public Author(){}
        
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