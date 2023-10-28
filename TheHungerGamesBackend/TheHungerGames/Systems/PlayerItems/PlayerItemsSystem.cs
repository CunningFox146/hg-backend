using System.Data.Entity;
using System.Data.Entity.Core;
using TheHungerGames.Data;
using TheHungerGames.Models;

namespace TheHungerGames.Systems.PlayerItems;

public class PlayerItemsSystem : IPlayerItemsSystem
{
    private readonly DataContext _dataContext;

    public PlayerItemsSystem(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task AddItems(string ownerId, IEnumerable<string> items)
    {
        var tasks = items.Select(item => AddItem(ownerId, item)).ToList();
        await Task.WhenAll(tasks);
        await _dataContext.SaveChangesAsync();
    }

    private async Task AddItem(string ownerId, string itemId)
    {
        var item = new PlayerItem
        {
            Id = Guid.NewGuid(),
            ItemId = itemId,
            OwnerId = ownerId,
            Registered = DateTime.UtcNow,
        };
        await _dataContext.AddAsync(item);
    }

    public async void RemoveItem(string ownerId, string itemId)
    {
        var item = _dataContext.PlayerItems.FirstOrDefaultAsync(p => p.ItemId == itemId && p.OwnerId == ownerId);
        if (item is null)
            throw new ObjectNotFoundException($"{ownerId} does not have any {itemId}s!");

        _dataContext.Remove(item);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<List<string>> GetPlayerItems(string ownerId) 
        => await _dataContext.PlayerItems.Where(p => p.OwnerId == ownerId).Select(p => p.ItemId).ToListAsync();
}