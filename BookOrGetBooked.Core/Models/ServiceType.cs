using BookOrGetBooked.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Models
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; }
        public string? CreatedByUserId { get; set; }
        public ApplicationUser? CreatedByUser { get; set; }
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
