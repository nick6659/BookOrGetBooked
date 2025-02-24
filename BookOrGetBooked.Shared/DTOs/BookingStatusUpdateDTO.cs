using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Shared.DTOs
{
    public class BookingStatusUpdateDTO
    {
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; }
        public int? CreatedByUserId { get; set; }
    }
}
