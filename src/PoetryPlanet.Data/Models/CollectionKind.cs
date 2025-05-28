using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoetryPlanet.Data.Models;

[Table("collection_kinds")]
public class CollectionKind
{
    public CollectionKind(){}
        
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