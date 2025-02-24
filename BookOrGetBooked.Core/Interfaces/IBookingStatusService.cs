using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IBookingStatusService : IGenericService<BookingStatus, BookingStatusCreateDTO, BookingStatusResponseDTO, BookingStatusUpdateDTO>
    {

    }
}
