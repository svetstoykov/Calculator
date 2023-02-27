namespace Calculator.Application.Common.Result.Models;

public class Result<T>
{
    public Result(T data, bool isSuccessful = false, string? message = null)
    {
        Data = data;
        Message = message ?? string.Empty;
        IsSuccessful = isSuccessful;
    }

    public bool IsSuccessful { get; }

    public T Data { get; }

    public string? Message { get; }
    
    public static Result<T> Success(T data, string? message = null) => new(data,true, message);

    public static Result<T> Failure(string? message = null) => new(default, false, message);
}