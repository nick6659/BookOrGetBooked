using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Models
{
    public class Currency
    {
        public int Id { get; set; }  // Primary key
        public required string Code { get; set; }  // ISO 4217 currency code, e.g., USD, EUR
        public required string Name { get; set; }  // Full name of the currency, e.g., United States Dollar
        public required string Symbol { get; set; }  // Currency symbol, e.g., $, €, £
        public bool IsEnabled { get; set; } = true;  // Allow enabling/disabling currencies
    }
}
