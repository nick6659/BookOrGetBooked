using BookOrGetBooked.Shared.DTOs.Profile;

namespace BookOrGetBooked.App.Shared.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileResponseDto?> GetProfileAsync();
        Task<bool> UpdateProfileAsync(UpdateUserProfileDto dto);
    }
}
