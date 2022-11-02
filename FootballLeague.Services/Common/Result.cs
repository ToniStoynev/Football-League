namespace FootballLeague.Services.Common;

public class Result
{
    private readonly List<string> _errors;

    internal Result(bool succeeded, List<string> errors)
    {
        Succeeded = succeeded;
        _errors = errors;
    }

    public bool Succeeded { get; }

    public List<string> Errors
            =>  Succeeded
                ? new List<string>()
                : _errors;

    public static Result Success
           => new Result(true, new List<string>());

    public static Result Failure(IEnumerable<string> errors)
            => new Result(false, errors.ToList());

}

public class Result<TData> : Result
{
    private readonly TData _data;

    internal Result(bool succeeded, TData data, List<string> errors)
        : base(succeeded, errors)
    {
        _data = data;
    }

    public TData Data
            => Succeeded
                ? _data
                : throw new InvalidOperationException(
                    $"{nameof(Data)} is not available with a failed result. Use {Errors} instead.");

    public static Result<TData> SuccessWith(TData data)
            => new Result<TData>(true, data, new List<string>());

    public new static Result<TData> Failure(IEnumerable<string> errors)
            => new Result<TData>(false, default!, errors.ToList());

    public static implicit operator Result<TData>(string error)
            => Failure(new List<string> { error });
}
