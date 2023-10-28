using TheHungerGames.Util;

namespace TheHungerGamesTests;

[TestOf(typeof(EloRatingCalculator))]
public class EloRatingTest
{
    [TestCase(1400, 1400, EloRatingCalculator.Win, EloRatingCalculator.Loose, ExpectedResult = 1416)]
    public double CheckWinDefault(double ratingA, double ratingB, double scoreA, double scoreB)
    {
        var a = EloRatingCalculator.CalculateNewRatings(ratingA, ratingB, scoreA, scoreB).ratingA;
        return EloRatingCalculator.CalculateNewRatings(ratingA, ratingB, scoreA, scoreB).ratingA;
    }
    
    [TestCase(1400, 1400, EloRatingCalculator.Win, EloRatingCalculator.Loose, ExpectedResult = 1400)]
    public double CheckLooseDefault(double ratingA, double ratingB, double scoreA, double scoreB)
    {
        return EloRatingCalculator.CalculateNewRatings(ratingA, ratingB, scoreA, scoreB).ratingB;
    }
    
    [TestCase(2150, 2450, EloRatingCalculator.Win, EloRatingCalculator.Loose, ExpectedResult = 1416)]
    public double CheckWinPro(double ratingA, double ratingB, double scoreA, double scoreB)
    {
        var a = EloRatingCalculator.CalculateNewRatings(ratingA, ratingB, scoreA, scoreB).ratingA;
        return EloRatingCalculator.CalculateNewRatings(ratingA, ratingB, scoreA, scoreB).ratingA;
    }
    
    [TestCase(1400, 1400, EloRatingCalculator.Win, EloRatingCalculator.Loose, ExpectedResult = 1400)]
    public double CheckLoosePro(double ratingA, double ratingB, double scoreA, double scoreB)
    {
        return EloRatingCalculator.CalculateNewRatings(ratingA, ratingB, scoreA, scoreB).ratingB;
    }
}