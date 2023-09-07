using System;
using showdown.Player;
using showdown.Utility;

namespace showdown.Team
{
	public class CustomTeam
	{
		public string Name { get; private set; }
		public TeamSettings TeamSettings { get; set; }
		public List<Batter> Batters { get; private set; }
        public List<Pitcher> Pitchers { get; private set; }
        public Dictionary<Position, IPlayer> Lineup { get; private set; }

        public CustomTeam(TeamSettings teamSettings, string name)
		{
			Name = name;
			TeamSettings = teamSettings;

			Batters = new List<Batter>();
			Pitchers = new List<Pitcher>();
			Lineup = new Dictionary<Position, IPlayer>();
		}

		public bool AddPlayer(IPlayer player, out string error)
		{
			if (!TeamSettings.ValidatePlayerAdd(Batters, Pitchers, Lineup,
				player, out error))
			{
				return false;
			} else
			{
				player.Selected = true;
			}

			if (player is Batter)
			{
				Batters.Add((Batter)player);
			} else
			{
				Pitchers.Add((Pitcher)player);
			}
			return true;
		}

        public override string ToString()
        {
			List<string> toString = new List<string>{};

			toString.Add("BATTERS:");
			foreach(Batter batter in Batters)
			{
				toString.Add(batter.ToString());
			}
            toString.Add("PITCHERS:");
            foreach (Pitcher pitcher in Pitchers)
			{
				toString.Add(pitcher.ToString());
			}
			return string.Join("\n", toString);
        }
    }
}

