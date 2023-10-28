namespace TheHungerGames.Systems.PlayerItems;

public interface IPlayerItemsSystem
{
    Task AddItems(string ownerId, IEnumerable<string> items);
    void RemoveItem(string ownerId, string itemId);
    Task<List<string>> GetPlayerItems(string ownerId);
}