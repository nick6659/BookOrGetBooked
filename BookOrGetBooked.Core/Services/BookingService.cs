using AutoMapper;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;
using BookOrGetBooked.Shared.Utilities;
using BookOrGetBooked.Shared.Filters;
using Microsoft.Extensions.Logging;

namespace BookOrGetBooked.Core.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserService _userService;
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookingService> _logger;

        public BookingService(IBookingRepository bookingRepository, IUserService userService, IServiceService serviceService, IMapper mapper, ILogger<BookingService> logger)
        {
            _bookingRepository = bookingRepository;
            _userService = userService;
            _serviceService = serviceService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<BookingResponseDTO>> CreateBookingAsync(BookingCreateDTO bookingRequest)
        {
            try
            {
                // Use UserService to check if the user exists
                var userExists = await _userService.ExistsAsync(bookingRequest.ProviderId);
                if (!userExists.Data)
                {
                    return Result<BookingResponseDTO>.Failure(ErrorCodes.Resource.NotFound);
                }

                var service = await _serviceService.GetServiceAsync(bookingRequest.ServiceId);
                if (!service.IsSuccess || service.Data == null)
                {
                    return Result<BookingResponseDTO>.Failure(ErrorCodes.Resource.NotFound);
                }

                if (service.Data.ProviderId != bookingRequest.ProviderId)
                {
                    return Result<BookingResponseDTO>.Failure(ErrorCodes.Authentication.Forbidden);
                }

                var booking = _mapper.Map<Booking>(bookingRequest);
                booking.StatusId = 1; // Pending

                await _bookingRepository.AddAsync(booking);

                var bookingResponse = _mapper.Map<BookingResponseDTO>(booking);
                return Result<BookingResponseDTO>.Success(bookingResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a booking. Request: {@BookingRequest}", bookingRequest);
                return Result<BookingResponseDTO>.Failure(ErrorCodes.Server.InternalServerError);
            }
        }

        public async Task<Result<BookingResponseDTO>> GetBookingAsync(int bookingId)
        {
            try
            {
                var booking = await _bookingRepository.GetByIdAsync(bookingId);

                if (booking == null)
                {
                    return Result<BookingResponseDTO>.Failure(ErrorCodes.Resource.NotFound);
                }

                var bookingResponse = _mapper.Map<BookingResponseDTO>(booking);
                return Result<BookingResponseDTO>.Success(bookingResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching booking with ID {BookingId}.", bookingId);
                return Result<BookingResponseDTO>.Failure(ErrorCodes.Server.InternalServerError);
            }
        }

        public async Task<Result<IEnumerable<BookingResponseDTO>>> GetBookingsAsync(BookingFilterParameters filters)
        {
            try
            {
                // Step 1: Validate the filters
                var validationErrors = filters.Validate();
                if (validationErrors.Any())
                {
                    // Return validation errors with an appropriate error code
                    return Result<IEnumerable<BookingResponseDTO>>.Failure(ErrorCodes.Validation.ValidationError, validationErrors);
                }

                // Step 2: Fetch bookings
                var bookings = await _bookingRepository.GetBookingsAsync(filters);

                if (bookings == null || !bookings.Any())
                {
                    return Result<IEnumerable<BookingResponseDTO>>.Failure(ErrorCodes.Resource.NotFound);
                }

                var bookingDTOs = _mapper.Map<IEnumerable<BookingResponseDTO>>(bookings);
                return Result<IEnumerable<BookingResponseDTO>>.Success(bookingDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching bookings with filters: {@Filters}", filters);
                return Result<IEnumerable<BookingResponseDTO>>.Failure(ErrorCodes.Server.InternalServerError);
            }
        }

        public async Task<Result<BookingResponseDTO>> UpdateBookingAsync(int id, BookingUpdateDTO bookingUpdateRequest)
        {
            try
            {
                // Fetch the existing booking with its related status
                var existingBooking = await _bookingRepository.GetByIdAsync(id);
                if (existingBooking == null)
                {
                    return Result<BookingResponseDTO>.Failure(ErrorCodes.Resource.NotFound, "Booking not found.");
                }

                // Manually update only the provided fields
                if (bookingUpdateRequest.BookerId != 0) // Check if a valid BookerId is provided
                {
                    existingBooking.BookerId = bookingUpdateRequest.BookerId;
                }

                if (bookingUpdateRequest.ServiceId != 0) // Check if a valid ServiceId is provided
                {
                    existingBooking.ServiceId = bookingUpdateRequest.ServiceId;
                }

                if (bookingUpdateRequest.TimeSlot != default) // Check if a valid TimeSlot is provided
                {
                    existingBooking.TimeSlot = bookingUpdateRequest.TimeSlot;
                }

                if (bookingUpdateRequest.BookingStatusId != 0) // Check if a valid StatusId is provided
                {
                    existingBooking.StatusId = bookingUpdateRequest.BookingStatusId;
                }

                // Save changes
                await _bookingRepository.UpdateAsync(existingBooking);

                // Reload the entity with the updated Status (if necessary)
                existingBooking = await _bookingRepository.GetByIdAsync(id);

                // Map to response DTO
                var bookingResponse = _mapper.Map<BookingResponseDTO>(existingBooking);
                return Result<BookingResponseDTO>.Success(bookingResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating booking with ID {BookingId}.", id);
                return Result<BookingResponseDTO>.Failure(ErrorCodes.Server.InternalServerError);
            }
        }
    }
}
