using System.Collections;
using Microsoft.EntityFrameworkCore;
using TheHungerGames.Models;
using TheHungerGames.Systems.Rollback;

namespace TheHungerGames.Data;

public class DataContext : DbContext, IRollbackable
{
    private readonly List<(IEnumerable dbSet, Action<object>)> _dataToRollback;

    public DbSet<Player> Players => Set<Player>();
    public DbSet<PlayerWin> Wins => Set<PlayerWin>();
    public DbSet<GameServerCrash> GameServerCrashes => Set<GameServerCrash>();
    public DbSet<PlayerCrash> PlayerCrashes => Set<PlayerCrash>();
    public DbSet<GameModel> Games => Set<GameModel>();
    public DbSet<PlayerDeath> Deaths => Set<PlayerDeath>();
    public DbSet<PlayerJoin> PlayerJoins => Set<PlayerJoin>();
    public DbSet<PlayerRating> PlayerRating => Set<PlayerRating>();
    public DbSet<RatingDelta> RatingDelta => Set<RatingDelta>();
    public DbSet<PlayerItem> PlayerItems => Set<PlayerItem>();
    public DbSet<CharacterPick> CharacterPicks => Set<CharacterPick>();
    public DbSet<ArenaSurvivor> ArenaSurvivors => Set<ArenaSurvivor>();

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        _dataToRollback = new List<(IEnumerable dbSet, Action<object> removeAction)>
        {
            (Deaths, obj => Deaths.Remove((PlayerDeath)obj)),
            (Wins, obj => Wins.Remove((PlayerWin)obj)),
            (PlayerItems, obj => PlayerItems.Remove((PlayerItem)obj)),
            (CharacterPicks, obj => CharacterPicks.Remove((CharacterPick)obj)),
        };
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

    public Task Rollback(string sessionId, DateTime targetTime)
    {
        foreach (var (dbSet, removeAction) in _dataToRollback)
        {
            foreach (var record in dbSet)
            {
                if (record is ISessionRecorder sessionRecorder and IDateRegistered dateRegistered 
                    && sessionRecorder.SessionId == sessionId && dateRegistered.Registered > targetTime)
                    removeAction.Invoke(record);
            }
        }
        return Task.CompletedTask;
    }
}