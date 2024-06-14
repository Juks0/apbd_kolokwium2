using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_kol2.Models;

[Table("characters")]

public class Characters
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(120)]
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    
    public ICollection<Backpacks> Backpacks { get; set; } = new HashSet<Backpacks>();
    
    public ICollection<Character_Titles> CharacterTitles { get; set; } = new HashSet<Character_Titles>();
}