namespace Topic.Application.Primitives;

public class ResultOrError<TValue, TError>
{
    protected readonly TValue _value = default!;
    protected readonly TError _error = default!;

    protected ResultOrError(TValue value) => _value = value;
    protected ResultOrError(TError error) => _error = error;

    public bool IsError => !EqualityComparer<TError>.Default.Equals(_error, default);
    public bool IsSuccess => !IsError;

    public static implicit operator ResultOrError<TValue, TError>(TValue value) => new(value);
    public static implicit operator ResultOrError<TValue, TError>(TError error) => new(error);

    /// <summary>
    /// Extracts actual result.
    /// </summary>
    /// <param name="result">The result object.</param>
    public static explicit operator TValue(in ResultOrError<TValue, TError> result) =>
         result.IsSuccess ? result._value : throw new NullReferenceException(nameof(TValue));

    /// <summary>
    /// Extracts actual error.
    /// </summary>
    /// <param name="result">The result object.</param>
    public static explicit operator TError(in ResultOrError<TValue, TError> result) =>
         result.IsError ? result._error : throw new NullReferenceException(nameof(TError));

    public TResult Match<TResult>(Func<TValue, TResult> success, Func<TError, TResult> failure) =>
        IsError ? failure(_error) : success(_value);
}

public class QueryResult<TResult> : ResultOrError<TResult, Exception>
{
    protected QueryResult(TResult value) : base(value) { }
    protected QueryResult(Exception error) : base(error) { }

    public static implicit operator QueryResult<TResult>(TResult value) => new(value);
    public static implicit operator QueryResult<TResult>(Exception error) => new(error);
};

public class CommandResult<TResult> : ResultOrError<TResult, Exception>
{
    protected CommandResult(TResult value) : base(value) { }
    protected CommandResult(Exception error) : base(error) { }

    public static implicit operator CommandResult<TResult>(TResult value) => new(value);
    public static implicit operator CommandResult<TResult>(Exception error) => new(error);
};