using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_kol2.Models;

[Table("character_Titles")]
[PrimaryKey(nameof(CharacterId), nameof(TitleId))]
public class Character_Titles
{
    public int CharacterId { get; set; }
    public int TitleId { get; set; }
    public DateTime AcquiredAt { get; set; }
    
    [ForeignKey(nameof(CharacterId))]
    public Characters Characters { get; set; }
    [ForeignKey(nameof(TitleId))]
    public Titles Titles { get; set; }


}