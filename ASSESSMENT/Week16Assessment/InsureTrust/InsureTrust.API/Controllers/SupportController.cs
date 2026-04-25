using System.Security.Claims;
using InsureTrust.API.Common;
using InsureTrust.API.DTOs.Support;
using InsureTrust.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureTrust.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SupportController : ControllerBase
{
    private readonly ISupportService _svc;
    public SupportController(ISupportService svc) => _svc = svc;

    [HttpGet("my-queries")]
    public async Task<IActionResult> GetMyQueries()
        => Ok(ApiResponse<IEnumerable<SupportQueryDto>>.Ok(await _svc.GetMyQueriesAsync(GetUserId())));

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
        => Ok(ApiResponse<IEnumerable<SupportQueryDto>>.Ok(await _svc.GetAllQueriesAsync()));

    [HttpPost("submit")]
    public async Task<IActionResult> Submit([FromForm] CreateSupportQueryDto dto)
    {
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        var query = await _svc.SubmitQueryAsync(dto, GetUserId(), uploadPath);
        return StatusCode(201, ApiResponse<SupportQueryDto>.Created(query, "Support query submitted."));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("update/{ticketId}")]
    public async Task<IActionResult> UpdateStatus(int ticketId, [FromBody] UpdateSupportStatusDto dto)
    {
        var result = await _svc.UpdateStatusAsync(ticketId, dto);
        return Ok(ApiResponse<SupportQueryDto>.Ok(result, "Ticket status updated."));
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}
