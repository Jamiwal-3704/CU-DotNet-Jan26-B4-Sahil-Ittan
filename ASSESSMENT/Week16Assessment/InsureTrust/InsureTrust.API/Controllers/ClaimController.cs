using System.Security.Claims;
using InsureTrust.API.Common;
using InsureTrust.API.DTOs.Claim;
using InsureTrust.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureTrust.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClaimController : ControllerBase
{
    private readonly IClaimService _svc;
    public ClaimController(IClaimService svc) => _svc = svc;

    [HttpGet("my-claims")]
    public async Task<IActionResult> GetMyClaims()
        => Ok(ApiResponse<IEnumerable<ClaimDto>>.Ok(await _svc.GetMyClaimsAsync(GetUserId())));

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
        => Ok(ApiResponse<IEnumerable<ClaimDto>>.Ok(await _svc.GetAllClaimsAsync()));

    [HttpPost("submit/{policyId}")]
    public async Task<IActionResult> Submit(int policyId, [FromForm] SubmitClaimDto dto)
    {
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        var claim = await _svc.SubmitClaimAsync(policyId, dto, GetUserId(), uploadPath);
        return StatusCode(201, ApiResponse<ClaimDto>.Created(claim, "Claim submitted successfully."));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("update/{claimId}")]
    public async Task<IActionResult> Update(int claimId, [FromBody] UpdateClaimDto dto)
    {
        var claim = await _svc.UpdateClaimAsync(claimId, dto);
        return Ok(ApiResponse<ClaimDto>.Ok(claim, "Claim updated."));
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}
