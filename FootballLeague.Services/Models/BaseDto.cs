namespace FootballLeague.Services.Models;

public abstract class BaseDto<TId>
    where TId : struct
{
    public TId Id { get; set; }
}
