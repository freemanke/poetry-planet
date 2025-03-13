using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoetryPlanet.Web.Data.Models;

public class UserFavoriteWork
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("username")]
    [MaxLength(255)]
    public string Username { get; set; } = "";
    
    [Column("work_id")]
    public int WorkId { get; set; }
}