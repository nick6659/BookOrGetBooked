using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.Utilities;

namespace BookOrGetBooked.Core.Services
{
    public class PhoneNumberService : IPhoneNumberService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhoneNumberRepository _phoneNumberRepository;

        public PhoneNumberService(IUserRepository userRepository, IPhoneNumberRepository phoneNumberRepository)
        {
            _userRepository = userRepository;
            _phoneNumberRepository = phoneNumberRepository;
        }

        public async Task<Result<PhoneNumber>> CreatePhoneNumberAsync(int userId, string prefix, string number)
        {
            // Check if the user exists
            var userExists = await _userRepository.ExistsAsync(userId);
            if (!userExists)
            {
                return Result<PhoneNumber>.Failure(ErrorCodes.Resource.NotFound, "The specified user does not exist.");
            }

            // Create and save the phone number
            var phoneNumber = PhoneNumber.Create(prefix, number, userId);
            await _phoneNumberRepository.AddAsync(phoneNumber);

            return Result<PhoneNumber>.Success(phoneNumber);
        }

        public async Task<Result<PhoneNumber>> GetPhoneNumberByIdAsync(int phoneNumberId)
        {
            // Retrieve the phone number
            var phoneNumber = await _phoneNumberRepository.GetByIdAsync(phoneNumberId);
            if (phoneNumber == null)
            {
                return Result<PhoneNumber>.Failure(ErrorCodes.Resource.NotFound, "Phone number not found.");
            }

            return Result<PhoneNumber>.Success(phoneNumber);
        }

        public async Task<Result<bool>> DeletePhoneNumberAsync(int phoneNumberId)
        {
            // Retrieve the phone number
            var phoneNumber = await _phoneNumberRepository.GetByIdAsync(phoneNumberId);
            if (phoneNumber == null)
            {
                return Result<bool>.Failure(ErrorCodes.Resource.NotFound, "Phone number not found.");
            }

            // Delete the phone number
            await _phoneNumberRepository.DeleteAsync(phoneNumber);

            return Result<bool>.Success(true);
        }

        public async Task<Result<PhoneNumber>> UpdatePhoneNumberAsync(int phoneNumberId, int userId, string prefix, string number)
        {
            // Retrieve the phone number
            var phoneNumber = await _phoneNumberRepository.GetByIdAsync(phoneNumberId);
            if (phoneNumber == null)
            {
                return Result<PhoneNumber>.Failure(ErrorCodes.Resource.NotFound, "Phone number not found.");
            }

            // Update the phone number
            phoneNumber.Update(prefix, number, userId);
            await _phoneNumberRepository.UpdateAsync(phoneNumber);

            return Result<PhoneNumber>.Success(phoneNumber);
        }
    }
}
