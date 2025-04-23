using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using backend.Models;
using showdown.Player;
using showdown.Retrieval;
using showdown.Utility;

namespace backend.Services;

public class DraftService
{
    private const string DRAFT_FILE_PATH = "drafts.json";
    private Dictionary<string, DraftState> _activeStates = new();
    private readonly GetPlayerCache _playerCache;
    

    public DraftService()
    {
        LoadDrafts();
        // _cardSetVersion and _playerCache are no longer used here.
    }

    private void LoadDrafts()
    {
        if (File.Exists(DRAFT_FILE_PATH))
        {
            var json = File.ReadAllText(DRAFT_FILE_PATH);
            _activeStates = JsonSerializer.Deserialize<Dictionary<string, DraftState>>(json) ?? new();
        }
    }

    private async Task SaveDrafts()
    {
        var json = JsonSerializer.Serialize(_activeStates);
        await File.WriteAllTextAsync(DRAFT_FILE_PATH, json);
    }

    public async Task<DraftState> CreateDraft(string draftId, CardSetVersion cardSetVersion)
{
    var draft = new DraftState
    {
        DraftId = draftId,
        IsActive = true,
        CurrentRound = 1,
        CardSetVersion = cardSetVersion,
        AvailablePlayers = GetAllPlayers(cardSetVersion)
    };
        
        _activeStates[draftId] = draft;
        await SaveDrafts();
        return draft;
    }

    private List<IPlayer> GetAllPlayers(CardSetVersion cardSetVersion)
{
    return GetPlayerCache.GetAllPlayers(cardSetVersion);
}

    public async Task<bool> JoinDraft(string draftId, string userId)
    {
        if (!_activeStates.ContainsKey(draftId)) return false;
        
        var draft = _activeStates[draftId];
        if (!draft.ConnectedUsers.Contains(userId))
        {
            draft.ConnectedUsers.Add(userId);
            draft.UserSelections[userId] = new List<IPlayer>();
            if (string.IsNullOrEmpty(draft.CurrentTurn))
            {
                draft.CurrentTurn = userId;
            }
        }
        
        await SaveDrafts();
        return true;
    }

    public async Task<bool> SelectPlayer(string draftId, string userId, string playerId)
    {
        if (!_activeStates.ContainsKey(draftId)) return false;
        
        var draft = _activeStates[draftId];
        if (draft.CurrentTurn != userId) return false;

        var player = draft.AvailablePlayers.Find(p => p.Id == playerId);
        if (player == null) return false;

        draft.AvailablePlayers.Remove(player);
        draft.UserSelections[userId].Add(player);
        
        // Move to next user's turn
        var currentIndex = draft.ConnectedUsers.IndexOf(userId);
        var nextIndex = (currentIndex + 1) % draft.ConnectedUsers.Count;
        draft.CurrentTurn = draft.ConnectedUsers[nextIndex];
        
        if (nextIndex == 0) draft.CurrentRound++;
        
        await SaveDrafts();
        return true;
    }

    public DraftState GetDraftState(string draftId)
    {
        return _activeStates.GetValueOrDefault(draftId);
    }
}
