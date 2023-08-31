using System;
namespace showdown.Utility
{
	public enum DiceRoll
	{
		PU,
		SO,
		GB,
		FB,
		W,
		S,
		SPlus,
		DB,
		TR,
		HR,
		NONE,
	}

	public class Dice
	{
		public static List<DiceRoll> Order =
			new List<DiceRoll>
			{
				DiceRoll.SO,
				DiceRoll.PU,
				DiceRoll.GB,
				DiceRoll.FB,
				DiceRoll.W,
				DiceRoll.S,
				DiceRoll.SPlus,
				DiceRoll.DB,
				DiceRoll.TR,
				DiceRoll.HR
			};
	}
}


