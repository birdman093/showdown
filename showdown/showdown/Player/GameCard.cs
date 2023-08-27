using System;
using System.Collections.Generic;
using showdown.Utility;
using showdown.Retrieval;

namespace showdown.Player
{
    public class DieCard
    {

    }

	public class BatterGameCard: IGameCard
	{

        public int Points { get; private set; }
        public int OnBase { get; private set; }
        public int Speed { get; private set; }
        public List<Position> Positions { get; private set; }
        public Dictionary<Position, int> Fielding { get; private set; }
        public BatSide BatSide { get; private set; }


		public BatterGameCard(PlayerCardCSV playerCard)
		{
            Points = int.Parse(playerCard.Pts);
            OnBase = int.Parse(playerCard.OB_C);
            Speed = ParseSpeed(playerCard.Spd_IP);
            ParsePositions(playerCard.Pos);
            BatSide = ParseSide(playerCard.H);
            

		}

        public BatSide ParseSide(string h)
        {
            h = h.ToLower();

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
            if (speed.ToUpper() == "A")
            {
                return 20;
            }
            else if (speed.ToUpper() == "B")
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
            Positions = null;
            Fielding = null;

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
}

