using System;
using System.Collections.Generic;
using showdown.Utility;
using showdown.Retrieval;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;

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

		public BatterGameCard(PlayerCardCSV playerCard)
		{
            Points = int.Parse(playerCard.Pts);
            OnBase = int.Parse(playerCard.OB_C);
            Speed = ParseSpeed(playerCard.Spd_IP);
            ParsePositions(playerCard.Pos);
            BatSide = ParseSide(playerCard.H);
            DieCard = DieParser.ParseDieCard(playerCard);
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
                string partTrim = part.Trim();
                string[] parts2 = partTrim.Split('+');
                int fieldingValue = 0;
                if (parts2.Length == 2)
                {
                    int.TryParse(parts2[1], out fieldingValue);
                }

                string[] positions = parts2[0].Split("-");
                foreach (string positionSplit in positions)
                {
                    string positionTrim = positionSplit.Trim();
                    Position result = PositionInfo.ParsePositionPlayer(positionTrim);
                    if (result != Position.DH)
                    {
                        Fielding[result] = fieldingValue;
                    }
                }

            }
        }

        public override string ToString()
        {
            List<string> diceString = new List<string>();
            for (int idx = 0; idx < DieCard.Count; idx++)
            {
                diceString.Add($"{idx}:{DieCard[idx]}");

            }
            List<string> fieldString = new List<string>();
            foreach (Position position in Fielding.Keys.ToList())
            {
                fieldString.Add($"{PositionInfo.PositionToString[position]} + {Fielding[position]}");
            }


            return $"Die Card {string.Join(';', diceString)} - " +
                $"Fielding {string.Join(';', fieldString)}";
        }

	}

    public class PitcherGameCard : IGameCard
    {
        public int Points { get; private set; }
        public int Control { get; private set; }
        public int Innings { get; private set; }
        public List<Position> Positions { get; private set; }
        public Dictionary<Position, int> Fielding { get; private set; }
        public PitchSide PitchSide { get; private set; }
        public List<DiceRoll> DieCard { get; private set; }

        public PitcherGameCard(PlayerCardCSV playerCard)
        {
            Points = int.Parse(playerCard.Pts);
            Control = int.Parse(playerCard.OB_C);
            Innings = int.Parse(playerCard.Spd_IP);
            ParsePositions(playerCard.Pos);
            PitchSide = ParseSide(playerCard.H);
            DieCard = DieParser.ParseDieCard(playerCard);
        }

        public void ParsePositions(string position)
        {
            Positions = new List<Position> { Position.P };

        }

        public PitchSide ParseSide(string h)
        {
            h = h.ToLower().Trim();

            if (h == "r" || h == "right")
            {
                return PitchSide.Right;
            }
            else if (h == "l" || h == "left")
            {
                return PitchSide.Left;
            }
            else
            {
                return PitchSide.Right;
            }
        }

        public override string ToString()
        {
            List<string> diceString = new List<string>();
            for (int idx = 0; idx < DieCard.Count; idx++)
            {
                diceString.Add($"{idx}:{DieCard[idx]}");

            }

            return $"Die Card {string.Join(';', diceString)} - IP {Innings}";
        }

    }

    public class InvalidCSVException : Exception
    {
        public InvalidCSVException(string message) : base(message)
        {
                  
        }
    }
}

