using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Shared.DTOs;
using BookOrGetBooked.Shared.Utilities;
using AutoMapper;
using BookOrGetBooked.Shared.Filters;
using Microsoft.Extensions.Logging;
using BookOrGetBooked.Core.Models;

namespace BookOrGetBooked.Core.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceService> _logger;
        private readonly IUserService _userService;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper, ILogger<ServiceService> logger, IUserService userService)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }

        public async Task<Result<ServiceResponseDTO>> CreateServiceAsync(ServiceCreateDTO serviceCreateDto)
        {
            try
            {
                var provider = await _userService.GetByIdAsync(serviceCreateDto.ProviderId);
                if (provider == null)
                {
                    return Result<ServiceResponseDTO>.Failure(ErrorCodes.Resource.NotFound, "Provider not found.");
                }

                var service = Service.Create(
                    serviceCreateDto.Name,
                    serviceCreateDto.Description,
                    serviceCreateDto.Price,
                    serviceCreateDto.CurrencyId,
                    serviceCreateDto.ProviderId
                );

                await _serviceRepository.AddAsync(service);

                var response = _mapper.Map<ServiceResponseDTO>(service);
                return Result<ServiceResponseDTO>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating service.");
                return Result<ServiceResponseDTO>.Failure(ErrorCodes.Server.InternalServerError);
            }
        }

        // Check if a service exists
        public async Task<Result<bool>> ServiceExistsAsync(int serviceId)
        {
            try
            {
                var serviceExists = await _serviceRepository.ExistsAsync(serviceId);
                if (!serviceExists)
                {
                    return Result<bool>.Failure(ErrorCodes.Resource.NotFound, "Service not found.");
                }
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if service with ID {ServiceId} exists.", serviceId);
                return Result<bool>.Failure(ErrorCodes.Server.InternalServerError);
            }
        }

        // Retrieve service details by ID
        public async Task<Result<ServiceResponseDTO>> GetServiceAsync(int serviceId)
        {
            try
            {
                var service = await _serviceRepository.GetByIdAsync(serviceId);
                if (service == null)
                {
                    return Result<ServiceResponseDTO>.Failure(ErrorCodes.Resource.NotFound, "Service not found.");
                }

                var serviceDTO = _mapper.Map<ServiceResponseDTO>(service);
                return Result<ServiceResponseDTO>.Success(serviceDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching service with ID {ServiceId}.", serviceId);
                return Result<ServiceResponseDTO>.Failure(ErrorCodes.Server.InternalServerError);
            }
        }

        // Retrieve services by filters
        public async Task<Result<IEnumerable<ServiceResponseDTO>>> GetServicesAsync(ServiceFilterParameters filters)
        {
            try
            {
                // Validate filters
                if (filters == null)
                {
                    return Result<IEnumerable<ServiceResponseDTO>>.Failure(ErrorCodes.Validation.InvalidInput, "Filters cannot be null.");
                }

                if (filters.StartDate.HasValue && filters.EndDate.HasValue && filters.StartDate > filters.EndDate)
                {
                    return Result<IEnumerable<ServiceResponseDTO>>.Failure(ErrorCodes.Validation.InvalidFormat, "StartDate cannot be later than EndDate.");
                }

                // Fetch services using filters
                var services = await _serviceRepository.GetServicesAsync(filters);

                if (services == null || !services.Any())
                {
                    return Result<IEnumerable<ServiceResponseDTO>>.Failure(ErrorCodes.Resource.NotFound, "No services found for the given criteria.");
                }

                var serviceDTOs = _mapper.Map<IEnumerable<ServiceResponseDTO>>(services);
                return Result<IEnumerable<ServiceResponseDTO>>.Success(serviceDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching services with filters: {@Filters}", filters);
                return Result<IEnumerable<ServiceResponseDTO>>.Failure(ErrorCodes.Server.InternalServerError);
            }
        }
    }
}
