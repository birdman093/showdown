using System;
using showdown.Retrieval;
using showdown.Utility;

namespace showdown.Player
{
	public class PlayerFactory
	{
		public static List<string> PitcherNames = new List<string>
		{
			"Starter",
			"SP",
			"Closer",
			"CP",
			"Reliever",
			"RP"
		};


		public static IPlayer CreatePlayer(PlayerCardCSV playerCard,
			CardSetVersion cardSetVersion)
		{
			if (PitcherNames.Contains(playerCard.Pos.Trim()))
			{
				return PitcherFactory.CreatePlayer(playerCard, cardSetVersion);
			}
			else
			{
				return BatterFactory.CreatePlayer(playerCard, cardSetVersion);
			}
		}

	}

	public class BatterFactory
	{
		public static Batter CreatePlayer(PlayerCardCSV playerCard,
			CardSetVersion cardSetVersion)
		{
			BatterGameCard batterGameCard = new BatterGameCard(playerCard);

			return new Batter(
				playerCard.Name,
				playerCard.Team,
				cardSetVersion,
				batterGameCard
				);
		}
	}

	public class PitcherFactory
	{
        public static Pitcher CreatePlayer(PlayerCardCSV playerCard,
			CardSetVersion cardSetVersion)
        {
            PitcherGameCard pitcherGameCard = new PitcherGameCard(playerCard);

            return new Pitcher(
                playerCard.Name,
                playerCard.Team,
                cardSetVersion,
                pitcherGameCard
                );
        }

    }





}

