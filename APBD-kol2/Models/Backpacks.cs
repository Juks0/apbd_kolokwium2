using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_kol2.Models;


[Table("backpacks")]
[PrimaryKey(nameof(CharacterId), nameof(ItemId))]
public class Backpacks
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }
    
    [ForeignKey(nameof(CharacterId))]
    public Characters Characters { get; set; }
    [ForeignKey(nameof(ItemId))]
    public Items Items { get; set; }

}