namespace TheHungerGames.Util;

public static class EloRatingCalculator
{
    public const double Win = 1;
    public const double Loose = 0;
    public const double Draw = 0.5;
    public const double StartRating = 1400;

    private static (double scoreA, double scoreB) GetExpectedScores(double ratingA, double ratingB)
    {
        var expectedScoreA = 1 / (1 + Math.Pow(10, (ratingB - ratingA) / 400));
        var expectedScoreB = 1 / (1 + Math.Pow(10, (ratingA - ratingB) / 400));

        return (expectedScoreA, expectedScoreB);
    }

    private static double UpdateRating(double rating, double score, double expectedScore)
    {
        var kFactor = rating < 2100 ? 32 : rating < 2400 ? 24 : 16;
        return Math.Floor(rating + kFactor * (score - expectedScore));
    }

    public static (double ratingA, double ratingB) CalculateNewRatings(double ratingA, double ratingB,
        double scoreA, double scoreB)
    {
        var expectedScores = GetExpectedScores(ratingA, ratingB);
        return (Math.Max(UpdateRating(ratingA, scoreA, expectedScores.scoreA), StartRating),
            Math.Max(UpdateRating(ratingB, scoreB, expectedScores.scoreB), StartRating));
    }
}