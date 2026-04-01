using BariatricChefApp.Api.DTOs;
using BariatricChefApp.Api.Mapping;
using BariatricChefApp.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BariatricChefApp.Api.Controllers;

[ApiController]
[Route("api/user/profile")]
[Authorize]
public class UserProfileController : ControllerBase
{
    private readonly UserProfileService _profileService;

    public UserProfileController(UserProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet]
    public async Task<ActionResult<UserProfileDto>> GetProfile(CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var profile = await _profileService.GetProfileAsync(userId, cancellationToken);
        if (profile is null) return NotFound();
        return Ok(profile.ToDto());
    }

    [HttpPut]
    public async Task<ActionResult<UserProfileDto>> UpsertProfile(
        [FromBody] UserProfileUpdateRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var profile = request.ToDomain();
        profile.UserId = userId;

        var result = await _profileService.CreateOrUpdateProfileAsync(profile, cancellationToken);
        return Ok(result.ToDto());
    }

    private string GetUserId() =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub")
            ?? throw new UnauthorizedAccessException("User identifier claim not found.");
}
