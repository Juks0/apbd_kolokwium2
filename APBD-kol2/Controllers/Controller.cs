
using APBD_kol2.DTOs;
using APBD_kol2.Services;

namespace APBD_kol2.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/characters")]
public class Controller : ControllerBase
{
    private readonly IDbService _dbService;

    public Controller(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{characterId}")]
    public async Task<IActionResult> GetCharacterDetails(int characterId)
    {
        try
        {
            var characterDetails = await _dbService.GetCharacterData(characterId);
            return Ok(characterDetails);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpPost("{characterId}/backpacks")]
    public async Task<IActionResult> AddItemsToBackpack(int characterId, [FromBody] List<int> itemIds)
    {
        try
        {
            var newItemsDto = new NewItemsDto { itemIds = itemIds };
        
            var totalWeightToAdd = await _dbService.GetItemsTotalWeight(itemIds);
        
            var characterData = await _dbService.GetCharacterData(characterId);
        
            if (characterData.CurrentWeight + totalWeightToAdd > characterData.MaxWeight)
            {
                return BadRequest("Adding these items exceeds the maximum weight capacity.");
            }
        
            var addedItems = await _dbService.AddItemsToCharacter(characterId, newItemsDto);
        
            var backpackItems = await _dbService.GetItemsInCharacterBackpack(characterId);
        
            return Ok(backpackItems); 
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}