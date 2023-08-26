using System;
using System.Collections.Generic;

namespace showdown.Utility
{
	public class CardSet
	{
		public CardSetVersion CardSetVersion { get; private set; }

		public CardSet(CardSetVersion cardSetVersion)
		{
			CardSetVersion = cardSetVersion;
		}

		public static Dictionary<CardSetVersion, string> CardSetToString =
			new Dictionary<CardSetVersion, string> {
				{ CardSetVersion.Base00, "00" },
				{ CardSetVersion.PR00, "00 PR" }
			};

        public string CardSetDisplay(CardSetVersion cardSet)
        {
			if (CardSetToString.ContainsKey(cardSet))
			{
				return CardSetToString[cardSet];
			} else
			{
				return "";
			}
        }
    }

    public enum CardSetVersion{
		Base00, // Base 00' card set
		PR00,
	}

	
}

