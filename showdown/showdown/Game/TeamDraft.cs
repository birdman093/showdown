using System;
using showdown.Team;

namespace showdown.Game
{
	public class TeamDraft
	{
		public Dictionary<string, CustomTeam> Teams;
		public TeamSettings TeamSettings;

		public TeamDraft(string name)
		{
			Teams = new Dictionary<string, CustomTeam>();
			TeamSettings = new TeamSettings();
			Teams[name] = new CustomTeam(TeamSettings, name);
		}

	}
}

