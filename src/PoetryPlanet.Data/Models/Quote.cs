using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoetryPlanet.Data.Models;

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