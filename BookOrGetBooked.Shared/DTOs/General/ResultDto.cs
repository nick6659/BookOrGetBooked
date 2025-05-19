namespace BookOrGetBooked.Shared.DTOs.General
{
    public class ResultDto<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public ErrorDto? Error { get; set; }
        public List<ValidationErrorDto>? ValidationErrors { get; set; }
    }

    public class ErrorDto
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class ValidationErrorDto
    {
        public string Field { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
    }
}
