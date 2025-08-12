namespace Shared;

public sealed class Result<T>
{
    private Result()
    {

    }

    public bool IsSuccess { get; private set; }
    public T? Value { get; private set; }
    public string? Error { get; private set; }

    public static Result<T> Success(T value) => new() { IsSuccess = true, Value = value };

    public static Result<T> Failure(string error) => new() { IsSuccess = false, Error = error };
}
