using System;
using showdown.Utility;
using showdown.Player;

namespace showdown.Retrieval
{
    public sealed class GetPlayerCache
    {
        #region Cache
        private static Dictionary<CardSetVersion, GetPlayerCache> _Instance;

        public static GetPlayerCache CheckInstance(CardSetVersion cardSetVersion)
        {
            if (_Instance == null)
            {
                DeserializeCSV = new DeserializeCSV();
                _Instance = new Dictionary<CardSetVersion, GetPlayerCache>();
                _Instance[cardSetVersion] = new GetPlayerCache(cardSetVersion);
                return _Instance[cardSetVersion];
            }
            else if (_Instance.ContainsKey(cardSetVersion))
            {
                return _Instance[cardSetVersion];
            }
            else
            {
                _Instance[cardSetVersion] = new GetPlayerCache(cardSetVersion);
                return _Instance[cardSetVersion];
            }
        }

        private GetPlayerCache(CardSetVersion cardSetVersion)
        {
            CardSetVersion = cardSetVersion;
            foreach (var card in DeserializeCSV.Deserialize(cardSetVersion))
            {
                PlayerCardCache.Add(PlayerFactory.CreatePlayer(card, cardSetVersion));
            }
        }

        #endregion

        #region Player Retrieval

        private static DeserializeCSV DeserializeCSV = new DeserializeCSV();
        private CardSetVersion CardSetVersion;
        private List<IPlayer> PlayerCardCache;

        public static IPlayer GetPlayer(CardSetVersion cardSet, string name)
        {
            return CheckInstance(cardSet)?.PlayerCardCache?.FirstOrDefault(x => x.Name == name);
        }

        public static List<IPlayer> GetAllPlayers(CardSetVersion cardSet)
        {
            return CheckInstance(cardSet)?.PlayerCardCache;
        }
        #endregion Player
    }

}

