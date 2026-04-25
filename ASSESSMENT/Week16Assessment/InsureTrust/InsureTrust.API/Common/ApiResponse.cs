namespace InsureTrust.API.Common;

/// <summary>
/// Standard API response wrapper used across all endpoints.
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public int StatusCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public IEnumerable<string>? Errors { get; set; }

    public static ApiResponse<T> Ok(T data, string message = "Request successful.")
        => new() { Success = true, Message = message, Data = data, StatusCode = 200 };

    public static ApiResponse<T> Created(T data, string message = "Resource created successfully.")
        => new() { Success = true, Message = message, Data = data, StatusCode = 201 };

    public static ApiResponse<T> Fail(string message, int statusCode = 400, IEnumerable<string>? errors = null)
        => new() { Success = false, Message = message, StatusCode = statusCode, Errors = errors };

    public static ApiResponse<T> NoContent(string message = "Operation completed.")
        => new() { Success = true, Message = message, StatusCode = 204 };
}
