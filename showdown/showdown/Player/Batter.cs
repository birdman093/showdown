using System;
using showdown.Retrieval;
using showdown.Utility;

namespace showdown.Player
{
	public class Batter : IPlayer
	{
        public string Name { get; private set;  }
        public string Team { get; private set; }
        public CardSetVersion CardSetVersion { get; private set; }
        public IGameCard GameCard { get; private set; }

        public Batter(string name, string team, CardSetVersion cardSetVersion,
            BatterGameCard gameCard)
        {
            Name = name;
            Team = team;
            CardSetVersion = cardSetVersion;
            GameCard = gameCard;
        }
	}
}

