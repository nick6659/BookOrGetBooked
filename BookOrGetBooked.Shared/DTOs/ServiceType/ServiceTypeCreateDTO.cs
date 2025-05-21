using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Shared.DTOs.ServiceType
{
    public class ServiceTypeCreateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; }
        public string? CreatedByUserId { get; set; }
    }
}
