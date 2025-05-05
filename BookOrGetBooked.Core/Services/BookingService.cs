using AutoMapper;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.Utilities;
using BookOrGetBooked.Shared.Filters;
using Microsoft.Extensions.Logging;
using BookOrGetBooked.Shared.DTOs.Booking;

namespace BookOrGetBooked.Core.Services;

public class BookingService(
    IBookingRepository bookingRepository,
    IServiceService serviceService,
    IMapper mapper,
    ILogger<BookingService> logger
    ) : IBookingService
{
    public async Task<Result<BookingResponseDTO>> CreateBookingAsync(BookingCreateDTO bookingRequest)
    {
        try
        {
            var service = await serviceService.GetServiceAsync(bookingRequest.ServiceId);
            if (!service.IsSuccess || service.Data == null)
            {
                return Result<BookingResponseDTO>.Failure(ErrorCodes.Resource.NotFound);
            }

            var booking = mapper.Map<Booking>(bookingRequest);

            await bookingRepository.AddAsync(booking);

            // Fetch the booking again to ensure Status is included
            var bookingWithStatus = await bookingRepository.GetByIdAsync(booking.Id);

            var bookingResponse = mapper.Map<BookingResponseDTO>(bookingWithStatus);

            return Result<BookingResponseDTO>.Success(bookingResponse);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating a booking. Request: {@BookingRequest}", bookingRequest);
            return Result<BookingResponseDTO>.Failure(ErrorCodes.Server.InternalServerError);
        }
    }

    public async Task<Result<BookingResponseDTO>> GetBookingAsync(int bookingId)
    {
        try
        {
            var booking = await bookingRepository.GetByIdAsync(bookingId);

            if (booking == null)
            {
                return Result<BookingResponseDTO>.Failure(ErrorCodes.Resource.NotFound);
            }

            var bookingResponse = mapper.Map<BookingResponseDTO>(booking);
            return Result<BookingResponseDTO>.Success(bookingResponse);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching booking with ID {BookingId}.", bookingId);
            return Result<BookingResponseDTO>.Failure(ErrorCodes.Server.InternalServerError);
        }
    }

    public async Task<Result<IEnumerable<BookingResponseDTO>>> GetBookingsAsync(BookingFilterParameters filters)
    {
        try
        {
            // Step 1: Validate the filters
            var validationErrors = filters.Validate();
            if (validationErrors.Count > 0)
            {
                // Return validation errors with an appropriate error code
                return Result<IEnumerable<BookingResponseDTO>>.Failure(ErrorCodes.Validation.ValidationError, validationErrors);
            }

            // Step 2: Fetch bookings
            var bookings = await bookingRepository.GetBookingsAsync(filters);

            if (bookings == null || !bookings.Any())
            {
                return Result<IEnumerable<BookingResponseDTO>>.Failure(ErrorCodes.Resource.NotFound);
            }

            var bookingDTOs = mapper.Map<IEnumerable<BookingResponseDTO>>(bookings);
            return Result<IEnumerable<BookingResponseDTO>>.Success(bookingDTOs);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching bookings with filters: {@Filters}", filters);
            return Result<IEnumerable<BookingResponseDTO>>.Failure(ErrorCodes.Server.InternalServerError);
        }
    }

    public async Task<Result<BookingResponseDTO>> UpdateBookingAsync(int id, BookingUpdateDTO bookingUpdateRequest)
    {
        try
        {
            // Fetch the existing booking with its related status
            var existingBooking = await bookingRepository.GetByIdAsync(id);
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
            await bookingRepository.UpdateAsync(existingBooking);

            // Reload the entity with the updated Status (if necessary)
            existingBooking = await bookingRepository.GetByIdAsync(id);

            // Map to response DTO
            var bookingResponse = mapper.Map<BookingResponseDTO>(existingBooking);
            return Result<BookingResponseDTO>.Success(bookingResponse);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating booking with ID {BookingId}.", id);
            return Result<BookingResponseDTO>.Failure(ErrorCodes.Server.InternalServerError);
        }
    }
}
