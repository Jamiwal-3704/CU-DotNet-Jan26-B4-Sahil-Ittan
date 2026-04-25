using System.Security.Claims;
using InsureTrust.API.Common;
using InsureTrust.API.DTOs.Notification;
using InsureTrust.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsureTrust.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _svc;
    public NotificationController(INotificationService svc) => _svc = svc;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(ApiResponse<IEnumerable<NotificationDto>>.Ok(await _svc.GetUserNotificationsAsync(GetUserId())));

    [HttpGet("unread-count")]
    public async Task<IActionResult> UnreadCount()
    {
        var count = await _svc.GetUnreadCountAsync(GetUserId());
        return Ok(ApiResponse<object>.Ok(new { count }));
    }

    [HttpPut("mark-read/{id}")]
    public async Task<IActionResult> MarkRead(int id)
    {
        await _svc.MarkReadAsync(id, GetUserId());
        return Ok(ApiResponse<object>.NoContent("Notification marked as read."));
    }

    [HttpPut("mark-all-read")]
    public async Task<IActionResult> MarkAllRead()
    {
        await _svc.MarkAllReadAsync(GetUserId());
        return Ok(ApiResponse<object>.NoContent("All notifications marked as read."));
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}
