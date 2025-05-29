using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoetryPlanet.Data.Models;

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