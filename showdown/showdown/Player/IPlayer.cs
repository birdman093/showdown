using System;
using showdown.Retrieval;
using showdown.Utility;

namespace showdown.Player
{
	public interface IPlayer
	{
        string Name { get; }
        string Team { get; }
        CardSetVersion CardSetVersion { get; }
        IGameCard GameCard { get;  }
        bool Selected { get; set; }
        string ToString();
    }
}

