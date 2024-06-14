using System.ComponentModel.DataAnnotations;

namespace APBD_kol2.Models;

public class Title
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    
    public ICollection<CharacterTitle> characterTitles { get; set; } = new HashSet<CharacterTitle>();
}