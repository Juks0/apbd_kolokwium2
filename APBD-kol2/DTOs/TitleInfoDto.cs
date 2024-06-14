
using System.ComponentModel.DataAnnotations;

namespace APBD_kol2.DTOs;

public class TitleInfoDto
{
    [Required]
    public string title { get; set; }
    [Required]
    public DateTime? acquiredAt { get; set; }
}