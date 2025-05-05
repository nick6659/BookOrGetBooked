using AutoMapper;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs.BookingStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Services
{
    public class BookingStatusService : GenericService<BookingStatus, BookingStatusCreateDTO, BookingStatusResponseDTO, BookingStatusUpdateDTO>, IBookingStatusService
    {
        public BookingStatusService(IBookingStatusRepository bookingStatusRepository, IMapper mapper)
            : base(bookingStatusRepository, mapper)
        {

        }

        // Add any currency-specific logic here if needed
    }
}
