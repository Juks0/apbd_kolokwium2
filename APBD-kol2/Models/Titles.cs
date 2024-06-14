using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_kol2.Models;

[Table("titles")]

public class Titles
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    
    public ICollection<Character_Titles> characterTitles { get; set; } = new HashSet<Character_Titles>();
}