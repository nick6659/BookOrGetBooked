using BookOrGetBooked.Infrastructure.Data;
using BookOrGetBooked.Shared.DTOs.Profile;
using BookOrGetBooked.Shared.Utilities;
using BookOrGetBooked.Shared.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookOrGetBooked.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ILogger<ProfileController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileController(ILogger<ProfileController> logger, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(Result.Failure(ErrorCodes.Authentication.Unauthorized));

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound(Result.Failure(ErrorCodes.Resource.NotFound, "User not found."));

        if (string.IsNullOrEmpty(user.Email))
            return BadRequest(Result.Failure(ErrorCodes.Validation.RequiredFieldMissing, "User email is missing."));

        var response = new UserProfileResponseDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            City = user.City,
            PostalCode = user.PostalCode,
            Country = user.Country
        };

        return Ok(Result<UserProfileResponseDto>.Success(response));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile(UpdateUserProfileDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(Result.Failure(ErrorCodes.Authentication.Unauthorized));

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return NotFound(Result.Failure(ErrorCodes.Resource.NotFound, "User not found."));

        // Update fields
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.PhoneNumber = dto.PhoneNumber;
        if (dto.Address != null) user.Address = dto.Address;
        if (dto.City != null) user.City = dto.City;
        if (dto.PostalCode != null) user.PostalCode = dto.PostalCode;
        if (dto.Country != null) user.Country = dto.Country;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => new ValidationError("Identity", ErrorCodes.Validation.InvalidInput, e.Description)).ToList();
            return BadRequest(Result.Failure(ErrorCodes.Validation.ValidationError, errors));
        }

        _logger.LogInformation("User {UserId} updated their profile.", userId);

        var updatedProfile = new UserProfileResponseDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            City = user.City,
            PostalCode = user.PostalCode,
            Country = user.Country
        };

        return Ok(Result<UserProfileResponseDto>.Success(updatedProfile));
    }
}
