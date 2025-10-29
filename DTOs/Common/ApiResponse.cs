using System.Text.Json.Serialization;

namespace Backend.DTOs.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public int StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string[]>? Errors { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            Success = statusCode is >= 200 and < 300;
            StatusCode = statusCode;
            Message = message;
        }

        public static ApiResponse<T> Fail(int statusCode, string message, Dictionary<string, string[]>? errors = null)
        {
            return new ApiResponse<T>(statusCode, message)
            {
                Errors = errors
            };
        }

        public static ApiResponse<T> Ok(T? data)
        {
            return new ApiResponse<T>(200)
            {
                Data = data
            };
        }
    }
}
