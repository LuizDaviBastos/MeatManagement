namespace MeatManager.Service.DTOs
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public static ServiceResult<T> Ok(T data, string message = "") =>
            new ServiceResult<T> { Success = true, Data = data, Message = message };

        public static ServiceResult<T> Fail(IEnumerable<string> errors, string message = "") =>
            new ServiceResult<T> { Success = false, Message = message, Errors = errors };

        public static ServiceResult<T> Fail(string message) =>
            new ServiceResult<T> { Success = false, Message = message };

        public static implicit operator ServiceResult<T>(T data) => Ok(data);
    }
}

public class ServiceResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public IEnumerable<string>? Errors { get; set; }
    public ServiceError ErrorCode { get; set; } = ServiceError.None;

    public static ServiceResult<T> Ok(T data, string message = "") =>
        new ServiceResult<T> { Success = true, Data = data, Message = message };

    public static ServiceResult<T> Fail(string message, ServiceError errorCode = ServiceError.None) =>
        new ServiceResult<T> { Success = false, Message = message, ErrorCode = errorCode };

    public static ServiceResult<T> Fail(IEnumerable<string> errors, ServiceError errorCode = ServiceError.None) =>
        new ServiceResult<T> { Success = false, Errors = errors, ErrorCode = errorCode };

    public static implicit operator ServiceResult<T>(T data) => Ok(data);
}

