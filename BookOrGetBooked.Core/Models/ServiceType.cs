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
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
