
using System.ComponentModel.DataAnnotations;

namespace APBD_kol2.DTOs;

public class BackpackInfoDto
{
    [Required]
    public string itemName { get; set; }
    [Required]
    public int itemWeight { get; set; }
    [Required]
    public int amount { get; set; }
}