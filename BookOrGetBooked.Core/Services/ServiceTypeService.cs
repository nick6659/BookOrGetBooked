using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;
using BookOrGetBooked.Shared.Utilities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Services
{
    public class ServiceTypeService(
        IServiceTypeRepository serviceTypeRepository,
        IMapper mapper,
        ILogger<ServiceTypeService> logger
    ) : GenericService<ServiceType, ServiceTypeCreateDTO, ServiceTypeResponseDTO, ServiceTypeUpdateDTO>(serviceTypeRepository, mapper), IServiceTypeService
    {
        private readonly IServiceTypeRepository _serviceTypeRepository = serviceTypeRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<ServiceTypeService> _logger = logger;

        public override async Task<Result<ServiceTypeResponseDTO>> UpdateAsync(int id, ServiceTypeUpdateDTO updateDto)
        {
            var existingServiceType = await _serviceTypeRepository.GetByIdAsync(id);
            if (existingServiceType == null)
            {
                return Result<ServiceTypeResponseDTO>.Failure(ErrorCodes.Resource.NotFound, "Service Type not found.");
            }

            // Ensure name uniqueness (optional)
            var allServiceTypes = await _serviceTypeRepository.GetAllAsync();
            if (allServiceTypes.Any(st => st.Name == updateDto.Name && st.Id != id))
            {
                return Result<ServiceTypeResponseDTO>.Failure(ErrorCodes.Validation.DuplicateEntry, "Service Type name must be unique.");
            }

            _mapper.Map(updateDto, existingServiceType);
            await _serviceTypeRepository.UpdateAsync(existingServiceType);

            var responseDto = _mapper.Map<ServiceTypeResponseDTO>(existingServiceType);
            return Result<ServiceTypeResponseDTO>.Success(responseDto);
        }
    }
}
