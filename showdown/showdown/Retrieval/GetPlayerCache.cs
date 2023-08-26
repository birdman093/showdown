using System;
using showdown.Utility;

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
            PlayerCardCache = null;
            
        }

        #endregion

        #region Player Retrieval

        private static DeserializeCSV DeserializeCSV;
        private CardSetVersion CardSetVersion;
        private List<PlayerCardCSV> PlayerCardCache;

        public static PlayerCardCSV GetPlayer(CardSetVersion cardSet, string name)
        {
            return new PlayerCardCSV();
        }

        public static List<PlayerCardCSV> GetSet(CardSetVersion cardSet)
        {
            return new List<PlayerCardCSV>();
        }
        #endregion Player
    }

}

