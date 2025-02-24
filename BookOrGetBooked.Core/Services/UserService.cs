using AutoMapper;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;
using BookOrGetBooked.Shared.Utilities;

namespace BookOrGetBooked.Core.Services
{
    public class UserService : GenericService<User, UserCreateDTO, UserResponseDTO, UserUpdateDTO>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserTypeService _userTypeService; // For validating UserType

        public UserService(IUserRepository userRepository, IMapper mapper, IUserTypeService userTypeService)
            : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userTypeService = userTypeService;
        }

        public override async Task<Result<UserResponseDTO>> CreateAsync(UserCreateDTO userCreateDto)
        {
            // Validate UserType
            var userTypeExists = await _userTypeService.ExistsAsync(userCreateDto.UserTypeId);

            if (!userTypeExists.Data)
            {
                return Result<UserResponseDTO>.Failure(ErrorCodes.Validation.InvalidInput, "Invalid User Type ID.");
            }

            // Create the User entity
            var user = User.Create(userCreateDto.Name, userCreateDto.Email, userCreateDto.UserTypeId);

            // Add Phone Numbers
            foreach (var phoneNumberDto in userCreateDto.PhoneNumbers)
            {
                var phoneNumber = PhoneNumber.Create(phoneNumberDto.Prefix, phoneNumberDto.Number, phoneNumberDto.UserId);
                user.PhoneNumbers.Add(phoneNumber);
            }

            // Save the User to the repository
            await _userRepository.AddAsync(user);

            // Map to response DTO
            var userResponseDto = _mapper.Map<UserResponseDTO>(user);

            return Result<UserResponseDTO>.Success(userResponseDto);
        }
    }
}
