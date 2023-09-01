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
        public int Points { get; private set; }
        public int Control { get; private set; }
        public int Innings { get; private set; }
        public List<Position> Positions { get; private set; }
        public Dictionary<Position, int> Fielding { get; private set; }
        public PitchSide PitchSide { get; private set; }
        public List<DiceRoll> DieCard { get; private set; }
        private PlayerCardCSV PlayerCard;

        public PitcherGameCard(PlayerCardCSV playerCard)
        {
            PlayerCard = playerCard;
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

        public string ToString()
        {
            return $"Die Card {DieCard} - Fielding {Fielding}";
        }

    }

    public class InvalidCSVException : Exception
    {
        public InvalidCSVException(string message) : base(message)
        {
                  
        }
    }
}

