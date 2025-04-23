using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Models;
using showdown.Player;
using showdown.Utility;
using System.Collections.Generic;

namespace backend.Controllers;

[ApiController]
[Route("Draft")]
public class DraftController : ControllerBase
{
    public class CreateDraftRequest
    {
        public string DraftId { get; set; }
        public CardSetVersion CardSetVersion { get; set; }
    }

    private readonly DraftService _draftService;

    public DraftController(DraftService draftService)
    {
        _draftService = draftService;
    }

    [HttpPost]
    public async Task<ActionResult<DraftState>> CreateDraft([FromBody] CreateDraftRequest request)
    {
        var draft = await _draftService.CreateDraft(request.DraftId, request.CardSetVersion);
        return Ok(draft);
    }

    [HttpGet("{draftId}")]
    public ActionResult<DraftState> GetDraftState(string draftId)
    {
        var draft = _draftService.GetDraftState(draftId);
        if (draft == null) return NotFound();
        return Ok(draft);
    }

    [HttpGet("{draftId}/available-players")]
    public ActionResult<List<IPlayer>> GetAvailablePlayers(string draftId)
    {
        var draft = _draftService.GetDraftState(draftId);
        if (draft == null) return NotFound();
        return Ok(draft.AvailablePlayers);
    }

    [HttpGet("{draftId}/user-selections/{userId}")]
    public ActionResult<List<IPlayer>> GetUserSelections(string draftId, string userId)
    {
        var draft = _draftService.GetDraftState(draftId);
        if (draft == null) return NotFound();
        if (!draft.UserSelections.ContainsKey(userId)) return NotFound();
        return Ok(draft.UserSelections[userId]);
    }
}

