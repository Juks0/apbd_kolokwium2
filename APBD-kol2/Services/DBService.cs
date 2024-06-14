using Microsoft.EntityFrameworkCore;
using APBD_kol2.DTOs;
using APBD_kol2.Models;

namespace APBD_kol2.Services
{
    public class DbService : IDbService
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<DbService> _logger;

        public DbService(DatabaseContext context, ILogger<DbService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> DoesCharacterExist(int characterId)
        {
            return await _context.Characters.AnyAsync(c => c.Id == characterId);
        }

        public async Task<CharacterInfoDto> GetCharacterData(int characterId)
        {
            if (!await DoesCharacterExist(characterId))
                throw new InvalidOperationException("Character doesn't exist");
    
            var character = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == characterId);

            if (character == null)
                throw new InvalidOperationException("Character doesn't exist");

            try
            {
                var backpackItems = await GetCharacterItems(characterId);
                var titles = await GetCharacterTitles(characterId);

                return new CharacterInfoDto
                {
                    FirstName = character.FirstName,
                    LastName = character.LastName,
                    CurrentWeight = character.CurrentWeight,
                    MaxWeight = character.MaxWeight,
                    backpackItems = backpackItems,
                    titles = titles
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while fetching character data for ID {characterId}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<BackpackInfoDto>> GetCharacterItems(int characterId)
        {
            var items = await _context.Backpacks
                .Where(b => b.CharacterId == characterId)
                .Include(b => b.Items)
                .Select(b => new BackpackInfoDto
                {
                    itemName = b.Items.Name,
                    itemWeight = b.Items.Weight,
                    amount = b.Amount
                })
                .ToListAsync();

            return items;
        }

        public async Task<List<TitleInfoDto>> GetCharacterTitles(int characterId)
        {
            var titles = await _context.CharacterTitles
                .Where(ct => ct.CharacterId == characterId)
                .Include(ct => ct.Titles)
                .Select(ct => new TitleInfoDto
                {
                    title = ct.Titles.Name,
                    acquiredAt = ct.AcquiredAt
                })
                .ToListAsync();

            return titles;
        }

      

        public async Task<int> GetItemsTotalWeight(ICollection<int> itemIds)
        {
            var totalWeight = await _context.Items
                .Where(i => itemIds.Contains(i.Id))
                .SumAsync(i => i.Weight);

            return totalWeight;
        }

  

 public async Task<List<AddedItemDto>> AddItemsToCharacter(int characterId, NewItemsDto items)
{
    try
    {
        var character = await _context.Characters
            .FirstOrDefaultAsync(c => c.Id == characterId);

        if (character == null)
        {
            _logger.LogWarning($"Character with ID {characterId} not found.");
            throw new ArgumentException("Character not found.");
        }

        var itemIds = items.itemIds;

        var totalWeightToAdd = await GetItemsTotalWeight(itemIds);

        if (character.CurrentWeight + totalWeightToAdd > character.MaxWeight)
        {
            throw new InvalidOperationException("Adding these items exceeds the maximum weight capacity.");
        }

        var existingBackpackItems = await _context.Backpacks
            .Where(b => b.CharacterId == characterId && itemIds.Contains(b.ItemId))
            .ToListAsync();

        var addedItems = new List<AddedItemDto>();

        foreach (var itemId in itemIds)
        {
            var existingItem = existingBackpackItems.FirstOrDefault(b => b.ItemId == itemId);

            if (existingItem != null)
            {
                existingItem.Amount++;
            }
            else
            {
                var backpack = new Backpacks()
                {
                    CharacterId = characterId,
                    ItemId = itemId,
                    Amount = 1
                };

                _context.Backpacks.Add(backpack);
            }

            addedItems.Add(new AddedItemDto
            {
                amount = 1,  
                itemId = itemId,
                characterId = characterId
            });
        }

        character.CurrentWeight += totalWeightToAdd;

        await _context.SaveChangesAsync();

        return addedItems;
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error while adding items to character with ID {characterId}: {ex.Message}");
        throw;
    }
}
 public async Task<int> GetItemsTotalWeight(List<int> itemIds)
 {
     var totalWeight = await _context.Items
         .Where(i => itemIds.Contains(i.Id))
         .SumAsync(i => i.Weight);

     return totalWeight;
 }

public async Task<List<AddedItemDto>> GetItemsInCharacterBackpack(int characterId)
{
    return await _context.Backpacks
        .Where(b => b.CharacterId == characterId)
        .Select(b => new AddedItemDto
        {
            amount = b.Amount,
            itemId = b.ItemId,
            characterId = b.CharacterId
        })
        .ToListAsync();
}
    }
    
}