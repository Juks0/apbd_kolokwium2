using APBD_kol2.DTOs;

namespace APBD_kol2.Services
{
    public interface IDbService
    {
        Task<CharacterInfoDto> GetCharacterData(int characterId);
        Task<List<AddedItemDto>> AddItemsToCharacter(int characterId, NewItemsDto items);
        Task<List<AddedItemDto>> GetItemsInCharacterBackpack(int characterId);
        Task<int> GetItemsTotalWeight(List<int> itemIds); 
    }
}