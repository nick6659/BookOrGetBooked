using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;
using BookOrGetBooked.Shared.Utilities;
using AutoMapper;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        // Check if a service exists
        public async Task<Result<bool>> ServiceExistsAsync(int serviceId)
        {
            var serviceExists = await _serviceRepository.ServiceExistsAsync(serviceId);
            if (!serviceExists)
            {
                return Result<bool>.Failure("Service not found");
            }
            return Result<bool>.Success(true);
        }

        // Retrieve service details by ID
        public async Task<Result<ServiceResponseDTO>> GetServiceByIdAsync(int serviceId)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(serviceId);
            if (service == null)
            {
                return Result<ServiceResponseDTO>.Failure("Service not found");
            }

            var serviceDTO = _mapper.Map<ServiceResponseDTO>(service);
            return Result<ServiceResponseDTO>.Success(serviceDTO);
        }
    }
}
