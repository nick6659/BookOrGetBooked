using AutoMapper;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Shared.DTOs;
using BookOrGetBooked.Shared.Utilities;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // Implementing UserExistsAsync method
        public async Task<Result<bool>> UserExistsAsync(int userId)
        {
            var userExists = await _userRepository.UserExistsAsync(userId);
            if (!userExists)
            {
                return Result<bool>.Failure("User not found");
            }
            return Result<bool>.Success(true);
        }

        // GetUserByIdAsync method
        public async Task<Result<UserResponseDTO>> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return Result<UserResponseDTO>.Failure("User not found");
            }

            var userDTO = _mapper.Map<UserResponseDTO>(user);
            return Result<UserResponseDTO>.Success(userDTO);
        }
    }
}
