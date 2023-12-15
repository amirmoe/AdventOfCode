using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions.Year2023.Day07;

internal class Day07 : ASolution
{
    private static readonly List<string> CardsWithoutJokers = new() { "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A" };
    private static readonly List<string> CardsWithJokers = new() { "J", "2", "3", "4", "5", "6", "7", "8", "9", "T", "Q", "K", "A" };

    public Day07() : base(07, 2023, "Camel Cards")
    {
    }


    protected override string SolvePartOne()
    {
        var hands = GetHands(Input);
        hands.Sort((a, b) => SortHands(a, b));
        var sum = 0;
        for (var i = 1; i <= hands.Count; i++) sum += hands[i - 1].Bid * i;
        return sum.ToString();
    }

    protected override string SolvePartTwo()
    {
        var hands = GetHands(Input);
        hands.Sort((a, b) => SortHands(a, b, true));
        var sum = 0;
        for (var i = 1; i <= hands.Count; i++) sum += hands[i - 1].Bid * i;
        return sum.ToString();
    }

    private static List<(string Hand, int Bid)> GetHands(string input)
    {
        return input
            .SplitByNewline()
            .Select(line =>
            {
                var values = line
                    .Split(" ");
                return (values.First(), int.Parse(values.Last()));
            })
            .ToList();
    }

    private static HandType DetermineHandType(string hand, bool convertJokers = false)
    {
        var uniqueLetters = hand
            .GroupBy(x => x)
            .Select(g => new Letter { Key = g.Key.ToString(), Count = g.Count() })
            .ToList();

        var jokers = uniqueLetters.FirstOrDefault(x => x.Key == "J");
        var amountOfJokers = jokers?.Count ?? 0;
        if (amountOfJokers > 0 && convertJokers)
        {
            var newLetter = uniqueLetters.Any(x => x.Key != "J")
                ? uniqueLetters.Where(x => x.Key != "J").MaxBy(x => x.Count)
                : uniqueLetters.MaxBy(x => x.Count);
            newLetter.Count += amountOfJokers;
            jokers!.Count -= amountOfJokers;
            if (jokers.Count == 0)
                uniqueLetters.Remove(jokers);
        }

        if (uniqueLetters.Count == 1)
            return HandType.FiveOfKind;
        if (uniqueLetters.Count == 2 && uniqueLetters.Any(x => x.Count == 4))
            return HandType.FourOfKind;
        if (uniqueLetters.Count == 2 && uniqueLetters.Any(x => x.Count == 3))
            return HandType.FullHouse;
        if (uniqueLetters.Any(x => x.Count == 3))
            return HandType.ThreeOfKind;
        if (uniqueLetters.Count(x => x.Count == 2) == 2)
            return HandType.TwoPair;
        if (uniqueLetters.Any(x => x.Count == 2))
            return HandType.OnePair;
        return HandType.HighCard;
    }

    private static int SortHands((string Hand, int Bid) a, (string Hand, int Bid) b, bool convertJokers = false)
    {
        var handTypeA = DetermineHandType(a.Hand, convertJokers);
        var handTypeB = DetermineHandType(b.Hand, convertJokers);

        if (handTypeA < handTypeB)
            return 1;
        if (handTypeA > handTypeB)
            return -1;

        for (var i = 0; i < a.Hand.Length; i++)
        {
            var cards = convertJokers ? CardsWithJokers : CardsWithoutJokers;
            var letterValueA = cards.FindIndex(x => x == a.Hand[i].ToString());
            var letterValueB = cards.FindIndex(x => x == b.Hand[i].ToString());

            if (letterValueA < letterValueB)
                return -1;
            if (letterValueA > letterValueB)
                return 1;
        }

        return 0;
    }
}

internal enum HandType
{
    FiveOfKind = 1,
    FourOfKind = 2,
    FullHouse = 3,
    ThreeOfKind = 4,
    TwoPair = 5,
    OnePair = 6,
    HighCard = 7
}

public class Letter
{
    public string Key { get; set; }
    public int Count { get; set; }
}