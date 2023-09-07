using System;
using showdown.Utility;

namespace showdown.Player
{
	public class Pitcher : IPlayer 
	{
        public string Name { get; private set; }
        public string Team { get; private set; }
        public CardSetVersion CardSetVersion { get; private set; }
        public IGameCard GameCard { get; private set; }
        public bool Selected { get; set; }

        public Pitcher(string name, string team, CardSetVersion cardSetVersion,
            PitcherGameCard gameCard)
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

