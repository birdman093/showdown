using System;
using System.Collections.Generic;
using showdown.Player;
using showdown.Utility;

namespace backend.Models;

public class DraftState
{
    public CardSetVersion CardSetVersion { get; set; }
    public string DraftId { get; set; }
    public List<string> ConnectedUsers { get; set; } = new();
    public Dictionary<string, List<IPlayer>> UserSelections { get; set; } = new();
    public List<IPlayer> AvailablePlayers { get; set; } = new();
    public string CurrentTurn { get; set; }
    public int CurrentRound { get; set; }
    public bool IsActive { get; set; }
}
