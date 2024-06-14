
using System.ComponentModel.DataAnnotations;

namespace APBD_kol2.DTOs;

public class CharacterInfoDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public int CurrentWeight { get; set; }
    [Required]
    public int MaxWeight { get; set; }
    [Required]
    public ICollection<BackpackInfoDto> backpackItems { get; set; }
    [Required]
    public ICollection<TitleInfoDto> titles { get; set; }
}




