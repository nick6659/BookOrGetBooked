using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Shared.Utilities
{
    public class ValidationError
    {
        public string Field { get; }
        public string Message { get; }
        public string Code { get; } // Optional error code

        public ValidationError(string field, string code, string message)
        {
            Field = field;
            Code = code;
            Message = message;
        }
    }
}
