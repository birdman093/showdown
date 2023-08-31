using System;
using System.Collections.Generic;
using showdown.Utility;
using showdown.Retrieval;

namespace showdown.Player
{

	public class BatterGameCard: IGameCard
	{

        public int Points { get; private set; }
        public int OnBase { get; private set; }
        public int Speed { get; private set; }
        public List<Position> Positions { get; private set; }
        public Dictionary<Position, int> Fielding { get; private set; }
        public BatSide BatSide { get; private set; }
        public List<DiceRoll> DieCard { get; private set; }
        private PlayerCardCSV PlayerCard;

		public BatterGameCard(PlayerCardCSV playerCard)
		{
            PlayerCard = playerCard;
            Points = int.Parse(playerCard.Pts);
            OnBase = int.Parse(playerCard.OB_C);
            Speed = ParseSpeed(playerCard.Spd_IP);
            ParsePositions(playerCard.Pos);
            BatSide = ParseSide(playerCard.H);
            ParseDieCard(playerCard);
        }

        public void ParseDieCard(PlayerCardCSV playerCard)
        {
            DieCard = new List<DiceRoll>
            {
                DiceRoll.NONE
            };


            var diceRollDict = playerCard.GetDiceDictionary();
            foreach(DiceRoll diceRoll in Dice.Order)
            {
                setRange(diceRoll, ParseRange(diceRollDict[diceRoll]));
            }
            

        }

        public Tuple<int, int> ParseRange(string input)
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

        public void setRange(DiceRoll diceRoll, Tuple<int,int> range)
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
                Console.WriteLine($"Invalid Chart: {PlayerCard.Name} - {DieCard.Count} != {range.Item1}");
                //throw new InvalidCSVException(PlayerCard.Name);
            }

            int setRange = range.Item2 - modifyStart + 1;

            for (int idx = 0; idx < setRange; idx++)
            {
                DieCard.Add(diceRoll);
            }
        }


        public BatSide ParseSide(string h)
        {
            h = h.ToLower().Trim();

            if (h == "r" || h == "right")
            {
                return BatSide.Right;
            }
            else if (h == "l" || h == "left")
            {
                return BatSide.Left;
            }
            else
            {
                return BatSide.Switch;
            }
        }

        public int ParseSpeed(string speed)
        {
            speed = speed.ToUpper().Trim();

            if (speed == "A")
            {
                return 20;
            }
            else if (speed == "B")
            {
                return 15;
            }
            else
            {
                return 10;
            }

        }

        public void ParsePositions(string position)
        {
            // Test Cases -> Pos: OF+0, Pos: 1B,  LF-RF+2, CF+3, LF-RF+2
            Positions = new List<Position>();
            Fielding = new Dictionary<Position, int>();

            string[] parts = position.Split(',');

            foreach (string part in parts)
            {
                // fielding Value
                part.Trim();
                string[] parts2 = part.Split('+');
                int fieldingValue = 0;
                if (parts2.Length == 2)
                {
                    int.TryParse(parts2[1], out fieldingValue);
                }

                string[] positions = parts2[0].Split("-");
                foreach (string postion in positions)
                {
                    position.Trim();
                    Enum.TryParse(position, true, out Position result);
                    Fielding[result] = fieldingValue; 
                }

            }
        }

        public string ToString()
        {
            return $"Die Card {DieCard} - Fielding {Fielding}";
        }

	}

    public class PitcherGameCard : IGameCard
    {


        public PitcherGameCard(string cardResults)
        {
        }


        public int maxValue()
        {
            return 0;
        }

    }

    public class InvalidCSVException : Exception
    {
        public InvalidCSVException(string message) : base(message)
        {
                  
        }
    }
}

