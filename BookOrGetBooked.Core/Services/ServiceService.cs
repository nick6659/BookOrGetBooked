using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Shared.DTOs;
using BookOrGetBooked.Shared.Utilities;
using AutoMapper;
using BookOrGetBooked.Shared.Filters;
using Microsoft.Extensions.Logging;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs.Service;

namespace BookOrGetBooked.Core.Services;

public class ServiceService(
    IServiceRepository serviceRepository,
    IMapper mapper,
    ILogger<ServiceService> logger,
    IGoogleDistanceService googleDistanceService
    ) : IServiceService
{
    public async Task<Result<ServiceResponseDTO>> CreateServiceAsync(ServiceCreateDTO serviceCreateDto)
    {
        try
        {
            var serviceCoverage = serviceCreateDto.ServiceCoverage != null
                ? new ServiceCoverage
                {
                    MaxDrivingDistanceKm = serviceCreateDto.ServiceCoverage.MaxDrivingDistanceKm,
                    MaxDrivingTimeMinutes = serviceCreateDto.ServiceCoverage.MaxDrivingTimeMinutes,
                    ProviderLatitude = serviceCreateDto.ServiceCoverage.ProviderLatitude,
                    ProviderLongitude = serviceCreateDto.ServiceCoverage.ProviderLongitude
                }
                : null;

            var service = Service.Create(
                serviceCreateDto.Name,
                serviceCreateDto.Description,
                serviceCreateDto.ServiceTypeId,
                serviceCreateDto.Price,
                serviceCreateDto.CurrencyId,
                serviceCreateDto.ProviderId,
                serviceCoverage
            );

            await serviceRepository.AddAsync(service);

            var response = mapper.Map<ServiceResponseDTO>(service);
            return Result<ServiceResponseDTO>.Success(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating service.");
            return Result<ServiceResponseDTO>.Failure(ErrorCodes.Server.InternalServerError);
        }
    }


    // Check if a service exists
    public async Task<Result<bool>> ServiceExistsAsync(int serviceId)
    {
        try
        {
            var serviceExists = await serviceRepository.ExistsAsync(serviceId);
            if (!serviceExists)
            {
                return Result<bool>.Failure(ErrorCodes.Resource.NotFound, "Service not found.");
            }
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while checking if service with ID {ServiceId} exists.", serviceId);
            return Result<bool>.Failure(ErrorCodes.Server.InternalServerError);
        }
    }

    // Retrieve service details by ID
    public async Task<Result<ServiceResponseDTO>> GetServiceAsync(int serviceId)
    {
        try
        {
            var service = await serviceRepository.GetByIdAsync(serviceId);
            if (service == null)
            {
                return Result<ServiceResponseDTO>.Failure(ErrorCodes.Resource.NotFound, "Service not found.");
            }

            var serviceDTO = mapper.Map<ServiceResponseDTO>(service);
            return Result<ServiceResponseDTO>.Success(serviceDTO);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching service with ID {ServiceId}.", serviceId);
            return Result<ServiceResponseDTO>.Failure(ErrorCodes.Server.InternalServerError);
        }
    }

    // Retrieve services by filters
    public async Task<Result<IEnumerable<ServiceResponseDTO>>> GetServicesAsync(ServiceFilterParameters filters)
    {
        var services = await serviceRepository.GetServicesAsync(filters);

        var serviceDTOs = services.Select(service => new ServiceResponseDTO
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description ?? "No description provided",
            Price = service.Price,
            ProviderId = service.ProviderId,
            ServiceType = new ServiceTypeResponseDTO
            {
                Id = service.ServiceType.Id,
                Name = service.ServiceType.Name
            },
            Currency = new CurrencyResponseDTO
            {
                Id = service.Currency.Id,
                Code = service.Currency.Code,
                Name = service.Currency.Name,
                Symbol = service.Currency.Symbol
            }
        }).ToList();

        return Result<IEnumerable<ServiceResponseDTO>>.Success(serviceDTOs);
    }

    public async Task<Result<IEnumerable<ServiceResponseDTO>>> GetAllServicesAsync()
    {
        var services = await serviceRepository.GetAllAsync();

        var serviceDTOs = services.Select(service => new ServiceResponseDTO
        {
            Id = service.Id,
            Name = service.Name,
            Description = service.Description ?? "No description provided",
            Price = service.Price,
            ProviderId = service.ProviderId,
            ServiceType = new ServiceTypeResponseDTO
            {
                Id = service.ServiceType.Id,
                Name = service.ServiceType.Name
            },
            Currency = new CurrencyResponseDTO
            {
                Id = service.Currency.Id,
                Code = service.Currency.Code,
                Name = service.Currency.Name,
                Symbol = service.Currency.Symbol
            }
        }).ToList();

        return Result<IEnumerable<ServiceResponseDTO>>.Success(serviceDTOs);
    }

    public async Task<Result<IEnumerable<ServiceResponseDTO>>> GetServicesWithinDistanceAsync(
        ServiceFilterParameters filters, double userLat, double userLon)
    {
        var services = await serviceRepository.GetServicesAsync(filters);
        var filteredServices = new List<ServiceResponseDTO>();

        foreach (var service in services)
        {
            if (service.ServiceCoverage == null)
            {
                filteredServices.Add(mapper.Map<ServiceResponseDTO>(service));
                continue;
            }

            var (distanceKm, durationMinutes) = await googleDistanceService.GetDrivingDistanceAsync(
                service.ServiceCoverage.ProviderLatitude,
                service.ServiceCoverage.ProviderLongitude,
                userLat, userLon
            );

            if (distanceKm <= service.ServiceCoverage.MaxDrivingDistanceKm)
            {
                filteredServices.Add(mapper.Map<ServiceResponseDTO>(service));
            }
        }

        return Result<IEnumerable<ServiceResponseDTO>>.Success(filteredServices);
    }

}
