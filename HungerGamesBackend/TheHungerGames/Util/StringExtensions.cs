namespace TheHungerGames.Util;

public static class StringExtensions
{
    public static bool IsPlayerId(this string str)
    {
        return str.StartsWith("KU_");
    }
}