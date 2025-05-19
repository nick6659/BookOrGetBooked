namespace BookOrGetBooked.Shared.Utilities
{
    public static class ErrorCodes
    {
        public static readonly Dictionary<string, string> AllMessages;

        static ErrorCodes()
        {
            AllMessages = new Dictionary<string, string>();

            // Merge all error code dictionaries into one
            foreach (var entry in Validation.Messages) AllMessages[entry.Key] = entry.Value;
            foreach (var entry in Authentication.Messages) AllMessages[entry.Key] = entry.Value;
            foreach (var entry in Resource.Messages) AllMessages[entry.Key] = entry.Value;
            foreach (var entry in Server.Messages) AllMessages[entry.Key] = entry.Value;
            foreach (var entry in BusinessLogic.Messages) AllMessages[entry.Key] = entry.Value;
            foreach (var entry in UserInput.Messages) AllMessages[entry.Key] = entry.Value;
        }

        public static class Validation
        {
            public const string ValidationError = "VALIDATION_ERROR";
            public const string RequiredFieldMissing = "REQUIRED_FIELD_MISSING";
            public const string InvalidFormat = "INVALID_FORMAT";
            public const string OutOfRange = "OUT_OF_RANGE";
            public const string UniqueConstraintViolation = "UNIQUE_CONSTRAINT_VIOLATION";
            public const string InvalidInput = "INVALID_INPUT";
            public const string DuplicateEntry = "DUPLICATE_ENTRY";
            public const string EmailAlreadyRegistered = "EMAIL_ALREADY_REGISTERED";
            public const string PhoneAlreadyRegistered = "PHONE_ALREADY_REGISTERED";

            public static readonly Dictionary<string, string> Messages = new()
            {
                { ValidationError, "The request contains validation errors." },
                { RequiredFieldMissing, "A required field is missing from the request." },
                { InvalidFormat, "The input format is invalid." },
                { OutOfRange, "The value is out of the acceptable range." },
                { UniqueConstraintViolation, "The value violates a unique constraint." },
                { InvalidInput, "The input provided is invalid." },
                { DuplicateEntry, "A record with the same value already exists. Please use a unique value." },
                { EmailAlreadyRegistered, "Email is already registered." },
                { PhoneAlreadyRegistered, "Phone number is already registered." },
            };
        }

        public static class Authentication
        {
            public const string Unauthorized = "UNAUTHORIZED";
            public const string Forbidden = "FORBIDDEN";
            public const string AuthenticationFailed = "AUTHENTICATION_FAILED";
            public const string SessionExpired = "SESSION_EXPIRED";
            public const string InvalidToken = "INVALID_TOKEN";

            public static readonly Dictionary<string, string> Messages = new()
            {
                { Unauthorized, "Access is denied due to invalid credentials." },
                { Forbidden, "Access is forbidden for the current user." },
                { AuthenticationFailed, "Authentication failed. Please verify your credentials." },
                { SessionExpired, "The session has expired. Please log in again." },
                { InvalidToken, "The provided token is invalid or expired." }
            };
        }

        public static class Resource
        {
            public const string NotFound = "NOT_FOUND";
            public const string Conflict = "CONFLICT";
            public const string ResourceLimitReached = "RESOURCE_LIMIT_REACHED";
            public const string ResourceAlreadyExists = "RESOURCE_ALREADY_EXISTS";

            public static readonly Dictionary<string, string> Messages = new()
            {
                { NotFound, "The requested resource could not be found." },
                { Conflict, "There is a conflict with the current state of the resource." },
                { ResourceLimitReached, "The resource limit has been reached." },
                { ResourceAlreadyExists, "The resource already exists." }
            };
        }

        public static class Server
        {
            public const string InternalServerError = "INTERNAL_SERVER_ERROR";
            public const string ServiceUnavailable = "SERVICE_UNAVAILABLE";
            public const string Timeout = "TIMEOUT";
            public const string DatabaseError = "DATABASE_ERROR";

            public static readonly Dictionary<string, string> Messages = new()
            {
                { InternalServerError, "An unexpected error occurred on the server." },
                { ServiceUnavailable, "The service is currently unavailable. Please try again later." },
                { Timeout, "The request timed out. Please try again." },
                { DatabaseError, "An error occurred while interacting with the database." }
            };
        }

        public static class BusinessLogic
        {
            public const string PaymentRequired = "PAYMENT_REQUIRED";
            public const string OrderAlreadyCompleted = "ORDER_ALREADY_COMPLETED";
            public const string AccountLocked = "ACCOUNT_LOCKED";
            public const string FeatureNotAvailable = "FEATURE_NOT_AVAILABLE";

            public static readonly Dictionary<string, string> Messages = new()
            {
                { PaymentRequired, "Payment is required to complete this operation." },
                { OrderAlreadyCompleted, "The order has already been completed." },
                { AccountLocked, "The account is locked. Please contact support." },
                { FeatureNotAvailable, "This feature is not available in the current version." }
            };
        }

        public static class UserInput
        {
            public const string InvalidInput = "INVALID_INPUT";
            public const string MalformedRequest = "MALFORMED_REQUEST";

            public static readonly Dictionary<string, string> Messages = new()
            {
                { InvalidInput, "The input provided is invalid." },
                { MalformedRequest, "The request is malformed or missing required fields." }
            };
        }
    }
}
