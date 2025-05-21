using BookOrGetBooked.Shared.Utilities;
using BookOrGetBooked.Shared.Validation;

namespace BookOrGetBooked.Shared.Filters
{
    public class BookingFilterParameters
    {
        public string? BookerId { get; set; } // The user who booked the service
        public string? ProviderId { get; set; } // The user who provides the service

        public int? ServiceId { get; set; }

        public DateTime? StartDate { get; set; } = null;

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                if (value.HasValue && StartDate.HasValue &&
                    StartDate.Value.Date == value.Value.Date &&
                    StartDate.Value.TimeOfDay != TimeSpan.Zero &&
                    value.Value.TimeOfDay != TimeSpan.Zero)
                {
                    // Use exact times
                    _endDate = value.Value;
                }
                else if (value.HasValue)
                {
                    // Include full day
                    _endDate = value.Value.Date.AddDays(1).AddTicks(-1);
                }
            }
        }

        public int? BookingStatusId { get; set; }
        public List<int> BookingIds { get; set; } = new List<int>();

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        /// <summary>
        /// Validates the filter parameters to ensure logical correctness.
        /// </summary>
        /// <returns>List of validation errors, empty if valid.</returns>
        public List<ValidationError> Validate()
        {
            var errors = new List<ValidationError>();

            if (!string.IsNullOrEmpty(BookerId) && !Guid.TryParse(BookerId, out _))
            {
                errors.Add(new ValidationError(nameof(BookerId), ErrorCodes.Validation.InvalidFormat, "BookerId must be a valid GUID."));
            }

            if (!string.IsNullOrEmpty(BookerId) && !Guid.TryParse(ProviderId, out _))
            {
                errors.Add(new ValidationError(nameof(BookerId), ErrorCodes.Validation.InvalidFormat, "ProviderId must be a valid GUID."));
            }

            // StartDate must not be after EndDate
            if (StartDate.HasValue && EndDate.HasValue && StartDate > EndDate)
            {
                errors.Add(new ValidationError(nameof(StartDate), ErrorCodes.Validation.InvalidFormat, "StartDate cannot be after EndDate."));
            }

            // BookingIds must contain only positive IDs
            if (BookingIds != null && BookingIds.Any(id => id <= 0))
            {
                errors.Add(new ValidationError(nameof(BookingIds), ErrorCodes.Validation.OutOfRange, "All BookingIds must be positive numbers."));
            }

            // Validate pagination parameters
            if (PageNumber.HasValue && PageNumber <= 0)
            {
                errors.Add(new ValidationError(nameof(PageNumber), ErrorCodes.Validation.OutOfRange, "PageNumber must be a positive number."));
            }

            if (PageSize.HasValue && PageSize <= 0)
            {
                errors.Add(new ValidationError(nameof(PageSize), ErrorCodes.Validation.OutOfRange, "PageSize must be a positive number."));
            }

            if (ServiceId.HasValue && ServiceId <= 0)
            {
                errors.Add(new ValidationError(nameof(ServiceId), ErrorCodes.Validation.OutOfRange, "ServiceId must be a positive number."));
            }

            return errors;
        }

    }
}
