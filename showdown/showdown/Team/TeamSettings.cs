using System;
using showdown.Player;
using showdown.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace showdown.Team
{
	public class TeamSettings
	{
		public int RosterSize { get; set; }
		public int PointLimit { get; set; }
		public int MinPitchers { get; set; }
		public int MinPositionBatters { get; set; }
		public bool FillEachPosition { get; set; }
		public List<CardSetVersion> AllowableCardVersions { get; set; }
        public bool AllowDuplicates { get; set; }
        public bool SettingsLock { get; set; }
        public string FirstPick { get; set; } //username

		public TeamSettings()
        {
            AllowableCardVersions = new List<CardSetVersion>();
            SettingsLock = false;
            FirstPick = "";
        }
        public bool ValidatePlayerAdd(List<Batter> batters,
            List<Pitcher> pitchers, Dictionary<Position, IPlayer> fielding
            ,IPlayer addedPlayer, out string error)
        {
            error = "";
            return true;
        }

        public bool ValidateFullRoster()
        {
            return true;
        }
    }
}

