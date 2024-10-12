using AutoMapper;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        // Using AutoMapper to map DTO to domain model
        public async Task<BookingResponseDTO> CreateBookingAsync(BookingRequestDTO bookingRequest)
        {
            // Use AutoMapper to map from DTO to the Booking domain model
            var booking = _mapper.Map<Booking>(bookingRequest);
            booking.Status = BookingStatus.Pending;

            // Save the new booking to the repository
            await _bookingRepository.AddBookingAsync(booking);

            // Use AutoMapper to map the Booking domain model to a response DTO
            return _mapper.Map<BookingResponseDTO>(booking);
        }

        public async Task<BookingResponseDTO> GetBookingByIdAsync(int bookingId)
        {
            // Get the booking from the repository
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);

            if (booking == null)
            {
                return null;  // Handle not found case
            }

            // Use AutoMapper to map the Booking domain model to a response DTO
            return _mapper.Map<BookingResponseDTO>(booking);
        }
    }
}
