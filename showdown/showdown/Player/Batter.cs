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
        public bool Selected { get; set; }

        //TODO: pass correct name to name and team
        public Batter(string name, string team, CardSetVersion cardSetVersion,
            BatterGameCard gameCard)
        {
            Name = name;
            Team = team;
            CardSetVersion = cardSetVersion;
            GameCard = gameCard;
        }

        public override string ToString()
        {
            return $"{Name}-{Team}-{CardSetVersion}-{GameCard}";
        }
	}
}

