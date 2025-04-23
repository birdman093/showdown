using System;
using showdown.Retrieval;
using showdown.Utility;

namespace showdown.Player
{
	public interface IPlayer
	{
        string Id { get; }
        string Name { get; }
        string Team { get; }
        CardSetVersion CardSetVersion { get; }
        IGameCard GameCard { get;  }
        bool Selected { get; set; }
        string ToString();
    }
}

