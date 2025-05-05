using AutoMapper;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs.User;
using BookOrGetBooked.Shared.Utilities;

namespace BookOrGetBooked.Core.Services
{
    public class UserService : GenericService<User, UserCreateDTO, UserCreatedDTO, UserUpdateDTO>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
            : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public override async Task<Result<UserCreatedDTO>> CreateAsync(UserCreateDTO userCreateDto)
        {
            var user = User.Create(userCreateDto.FirstName, userCreateDto.LastName, userCreateDto.Email);

            var phoneNumberDto = userCreateDto.PhoneNumber;
            var phoneNumber = PhoneNumber.Create(phoneNumberDto.Prefix, phoneNumberDto.Number);
            user.PhoneNumbers.Add(phoneNumber);

            await _userRepository.AddAsync(user);

            // Convert to UserCreatedDTO instead of UserResponseDTO
            var userCreatedDto = _mapper.Map<UserCreatedDTO>(user);

            return Result<UserCreatedDTO>.Success(userCreatedDto);
        }

    }
}
