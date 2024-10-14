using AutoMapper;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;
using BookOrGetBooked.Shared.Utilities;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserService _userService;
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IUserService userService, IServiceService serviceService, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _userService = userService;
            _serviceService = serviceService;
            _mapper = mapper;
        }

        public async Task<Result<BookingResponseDTO>> CreateBookingAsync(BookingRequestDTO bookingRequest)
        {
            // Use UserService to check if the user exists
            var userExists = await _userService.UserExistsAsync(bookingRequest.UserId);
            if (!userExists.Data)
            {
                return Result<BookingResponseDTO>.Failure("Invalid User ID");
            }

            var service = await _serviceService.GetServiceByIdAsync(bookingRequest.ServiceId);
            if (!service.IsSuccess || service.Data == null)
            {
                return Result<BookingResponseDTO>.Failure("Invalid Service ID");
            }

            if (service.Data.ProviderId != bookingRequest.UserId)
            {
                return Result<BookingResponseDTO>.Failure("User is not the provider of this service");
            }

            var booking = _mapper.Map<Booking>(bookingRequest);
            booking.Status = BookingStatus.Pending;

            await _bookingRepository.AddBookingAsync(booking);

            var bookingResponse = _mapper.Map<BookingResponseDTO>(booking);
            return Result<BookingResponseDTO>.Success(bookingResponse);
        }

        public async Task<Result<BookingResponseDTO>> GetBookingByIdAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);

            if (booking == null)
            {
                return Result<BookingResponseDTO>.Failure("Booking not found");
            }

            var bookingResponse = _mapper.Map<BookingResponseDTO>(booking);
            return Result<BookingResponseDTO>.Success(bookingResponse);
        }
    }
}
