using System;
using showdown.Retrieval;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace showdown.Utility
{
	public class DieParser
	{
        public static List<DiceRoll> ParseDieCard(PlayerCardCSV playerCard)
        {
            List<DiceRoll> DieCard = new List<DiceRoll>
            {
                DiceRoll.NONE
            };


            var diceRollDict = playerCard.GetDiceDictionary();
            foreach (DiceRoll diceRoll in Dice.Order)
            {
                setRange(diceRoll, ParseRange(diceRollDict[diceRoll]), DieCard,
                    playerCard.Name);
            }

            return DieCard;
        }

        public static Tuple<int, int> ParseRange(string input)
        {
            input = input.Trim();
            input = input.Replace("+", "");
            string[] parts = input.Split('-');
            parts = Array.FindAll(parts, part => !string.IsNullOrEmpty(part));
            int start = 0;
            int end = 0;

            if (parts.Length == 1)
            {
                start = int.Parse(parts[0]);
                end = start;
            }
            else if (parts.Length == 2)
            {
                start = int.Parse(parts[0]);
                end = int.Parse(parts[1]);
            }

            return new Tuple<int, int>(start, end);
        }

        public static void setRange(DiceRoll diceRoll, Tuple<int, int> range,
            List<DiceRoll> DieCard, string name)
        {
            if (range.Item1 == 0 || range.Item2 == 0
                || range.Item1 > range.Item2)
            {
                return;
            }

            int modifyStart = range.Item1;
            if (DieCard.Count != range.Item1)
            {
                modifyStart = DieCard.Count;
                Console.WriteLine($"Invalid Chart: {name} - {DieCard.Count} != {range.Item1}");
                //throw new InvalidCSVException(PlayerCard.Name);
            }

            int setRange = range.Item2 - modifyStart + 1;

            for (int idx = 0; idx < setRange; idx++)
            {
                DieCard.Add(diceRoll);
            }
        }


    }
}

