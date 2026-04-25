using InsureTrust.API.Common;
using InsureTrust.API.DTOs.Calculator;
using InsureTrust.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsureTrust.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculatorService _svc;
    public CalculatorController(ICalculatorService svc) => _svc = svc;

    /// <summary>Estimate insurance premium — no auth required (public endpoint).</summary>
    [HttpPost("estimate")]
    public IActionResult Estimate([FromBody] CalculatorRequestDto dto)
    {
        var result = _svc.Estimate(dto);
        return Ok(ApiResponse<CalculatorResultDto>.Ok(result, "Premium estimate calculated."));
    }
}
