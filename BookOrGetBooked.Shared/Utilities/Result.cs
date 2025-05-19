namespace BookOrGetBooked.Shared.Utilities
{
    using BookOrGetBooked.Shared.Validation;
    using System.Text.Json.Serialization;

    public class Result
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ErrorDetails? Error { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] // Exclude when null
        public List<ValidationError>? ValidationErrors { get; }

        // Protected constructor to allow derived classes (e.g., Result<T>) to use it
        protected Result(bool isSuccess, ErrorDetails? error = null, List<ValidationError>? validationErrors = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            ValidationErrors = validationErrors?.Any() == true ? validationErrors : null;
        }

        // Static factory methods
        public static Result Success() =>
            new Result(true);

        public static Result Failure(string code, List<ValidationError>? validationErrors = null)
        {
            string message = ErrorCodes.AllMessages.TryGetValue(code, out var defaultMessage)
                ? defaultMessage
                : "Unknown error.";
            return new Result(false, new ErrorDetails(code, message), validationErrors);
        }

        public static Result Failure(string code, string message, List<ValidationError>? validationErrors = null) =>
            new Result(false, new ErrorDetails(code, message), validationErrors);

        public bool HasValidationErrors => ValidationErrors?.Any() == true;

    }

    public class Result<T> : Result
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] // Exclude when null
        public T? Data { get; }

        // Protected constructor for Result<T>, calls base class constructor
        protected Result(bool isSuccess, T? data = default, ErrorDetails? error = null, List<ValidationError>? validationErrors = null)
            : base(isSuccess, error, validationErrors)
        {
            Data = data;
        }

        // Static factory methods
        public static Result<T> Success(T data) => new Result<T>(true, data);

        public static new Result<T> Failure(string code, List<ValidationError>? validationErrors = null)
        {
            string message = ErrorCodes.AllMessages.TryGetValue(code, out var defaultMessage)
                ? defaultMessage
                : "Unknown error.";
            return new Result<T>(false, default, new ErrorDetails(code, message), validationErrors);
        }

        public static new Result<T> Failure(string code, string message, List<ValidationError>? validationErrors = null) =>
            new Result<T>(false, default, new ErrorDetails(code, message), validationErrors);
    }
}
