using AutoMapper;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Services
{
    public class UserTypeService : GenericService<UserType, UserTypeCreateDTO, UserTypeResponseDTO, UserUpdateDTO>, IUserTypeService
    {
        private readonly IUserTypeRepository _userTypeRepository;

        public UserTypeService(IUserTypeRepository userTypeRepository, IMapper mapper)
            : base(userTypeRepository, mapper)
        {
            _userTypeRepository = userTypeRepository;
        }

        // Add user-type-specific logic here if needed
    }
}
