using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Infrastructure.Data;
using BookOrGetBooked.Shared.DTOs.Auth;
using BookOrGetBooked.Shared.Utilities;
using BookOrGetBooked.Shared.Validation;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BookOrGetBooked.Core.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<Result> RegisterAsync(RegisterRequestDto model, CancellationToken cancellationToken = default)
    {
        var existingEmailUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingEmailUser != null)
            return Result.Failure(ErrorCodes.Validation.EmailAlreadyRegistered);

        var existingPhoneUser = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == model.PhoneNumber);
        if (existingPhoneUser != null)
            return Result.Failure(ErrorCodes.Validation.PhoneAlreadyRegistered);

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            var validationErrors = result.Errors
                .Select(e => new ValidationError("Identity", ErrorCodes.Validation.InvalidInput, e.Description))
                .ToList();

            return Result.Failure(ErrorCodes.Validation.ValidationError, validationErrors);
        }

        return Result.Success();
    }

    public async Task<Result<TokenResponseDto>> LoginAsync(LoginRequestDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return Result<TokenResponseDto>.Failure(ErrorCodes.Authentication.AuthenticationFailed);

        var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
        if (!signInResult.Succeeded)
            return Result<TokenResponseDto>.Failure(ErrorCodes.Authentication.AuthenticationFailed);

        var rawRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = TokenHasher.Hash(rawRefreshToken);
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        return Result<TokenResponseDto>.Success(new TokenResponseDto
        {
            Token = _tokenService.GenerateToken(user),
            RefreshToken = rawRefreshToken
        });
    }

    public async Task<Result<TokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto model)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(model.Token);
        if (principal == null)
            return Result<TokenResponseDto>.Failure(ErrorCodes.Authentication.InvalidToken);

        var email = principal.FindFirstValue(ClaimTypes.Email);
        var user = await _userManager.FindByEmailAsync(email!);

        var incomingRefreshTokenHash = TokenHasher.Hash(model.RefreshToken);

        if (user is null || user.RefreshToken != incomingRefreshTokenHash || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return Result<TokenResponseDto>.Failure(ErrorCodes.Authentication.InvalidToken);

        var newJwt = _tokenService.GenerateToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        return Result<TokenResponseDto>.Success(new TokenResponseDto
        {
            Token = newJwt,
            RefreshToken = newRefreshToken
        });
    }

    public async Task<Result> LogoutAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.Failure(ErrorCodes.Resource.NotFound);

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await _userManager.UpdateAsync(user);

        return Result.Success();
    }

}
