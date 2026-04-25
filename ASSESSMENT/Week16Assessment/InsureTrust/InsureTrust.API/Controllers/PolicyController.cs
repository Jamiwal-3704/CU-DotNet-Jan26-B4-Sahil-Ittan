using System.Security.Claims;
using InsureTrust.API.Common;
using InsureTrust.API.DTOs.Policy;
using InsureTrust.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureTrust.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PolicyController : ControllerBase
{
    private readonly IPolicyService _svc;
    public PolicyController(IPolicyService svc) => _svc = svc;

    [HttpGet("types")]
    public async Task<IActionResult> GetTypes()
        => Ok(ApiResponse<IEnumerable<PolicyTypeDto>>.Ok(await _svc.GetPolicyTypesAsync()));

    [HttpGet("types/{id}")]
    public async Task<IActionResult> GetType(int id)
        => Ok(ApiResponse<PolicyTypeDto>.Ok(await _svc.GetPolicyTypeByIdAsync(id)));

    [Authorize(Roles = "Admin")]
    [HttpPost("types")]
    public async Task<IActionResult> CreateType([FromBody] CreatePolicyTypeDto dto)
    {
        var pt = await _svc.CreatePolicyTypeAsync(dto);
        return StatusCode(201, ApiResponse<PolicyTypeDto>.Created(pt, "Policy type created successfully."));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("types/{id}")]
    public async Task<IActionResult> UpdateType(int id, [FromBody] CreatePolicyTypeDto dto)
    {
        var pt = await _svc.UpdatePolicyTypeAsync(id, dto);
        return Ok(ApiResponse<PolicyTypeDto>.Ok(pt, "Policy type updated."));
    }

    [Authorize]
    [HttpGet("my-policies")]
    public async Task<IActionResult> GetMyPolicies()
    {
        var policies = await _svc.GetMyPoliciesAsync(GetUserId());
        return Ok(ApiResponse<IEnumerable<PolicyDto>>.Ok(policies));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
        => Ok(ApiResponse<IEnumerable<PolicyDto>>.Ok(await _svc.GetAllPoliciesAsync()));

    [Authorize(Roles = "Admin")]
    [HttpGet("pending")]
    public async Task<IActionResult> GetPending()
        => Ok(ApiResponse<IEnumerable<PolicyDto>>.Ok(await _svc.GetPendingPoliciesAsync()));

    [Authorize]
    [HttpPost("purchase")]
    public async Task<IActionResult> Purchase([FromBody] CreatePolicyDto dto)
    {
        var policy = await _svc.PurchaseAsync(dto, GetUserId());
        return StatusCode(201, ApiResponse<PolicyDto>.Created(policy, "Policy submitted for approval."));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("approve/{policyId}")]
    public async Task<IActionResult> Approve(int policyId, [FromBody] ApprovePolicyDto dto)
    {
        var policy = await _svc.ApprovePolicyAsync(policyId, dto, GetUserId());
        return Ok(ApiResponse<PolicyDto>.Ok(policy, $"Policy {dto.Action}ed successfully."));
    }

    [Authorize]
    [HttpPut("edit/{policyId}")]
    public async Task<IActionResult> Edit(int policyId, [FromBody] EditPolicyDto dto)
    {
        var policy = await _svc.EditPolicyAsync(policyId, dto, GetUserId());
        return Ok(ApiResponse<PolicyDto>.Ok(policy, "Policy updated."));
    }

    [Authorize]
    [HttpPost("renew/{policyId}")]
    public async Task<IActionResult> Renew(int policyId)
    {
        var policy = await _svc.RenewPolicyAsync(policyId, GetUserId());
        return Ok(ApiResponse<PolicyDto>.Ok(policy, "Policy renewed successfully."));
    }

    [Authorize]
    [HttpDelete("{policyId}")]
    public async Task<IActionResult> Delete(int policyId)
    {
        var role = User.FindFirst(ClaimTypes.Role)!.Value;
        await _svc.DeletePolicyAsync(policyId, GetUserId(), role);
        return Ok(ApiResponse<object>.NoContent("Policy deleted."));
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}
