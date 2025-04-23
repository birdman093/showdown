using Microsoft.AspNetCore.SignalR;
using backend.Services;
using backend.Models;

namespace backend.Hubs;

public class DraftHub : Hub
{
    private readonly DraftService _draftService;

    public DraftHub(DraftService draftService)
    {
        _draftService = draftService;
    }

    public async Task CreateDraft(string draftId, showdown.Utility.CardSetVersion cardSetVersion)
    {
        var draft = await _draftService.CreateDraft(draftId, cardSetVersion);
        await Groups.AddToGroupAsync(Context.ConnectionId, draftId);
        await Clients.Group(draftId).SendAsync("DraftStateUpdated", draft);
    }

    public async Task JoinDraft(string draftId, string userId)
    {
        var success = await _draftService.JoinDraft(draftId, userId);
        if (success)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, draftId);
            var draft = _draftService.GetDraftState(draftId);
            await Clients.Group(draftId).SendAsync("DraftStateUpdated", draft);
        }
    }

    public async Task SelectPlayer(string draftId, string userId, string playerId)
    {
        var success = await _draftService.SelectPlayer(draftId, userId, playerId);
        if (success)
        {
            var draft = _draftService.GetDraftState(draftId);
            await Clients.Group(draftId).SendAsync("DraftStateUpdated", draft);
        }
    }

    public async Task LeaveDraft(string draftId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, draftId);
    }
}
