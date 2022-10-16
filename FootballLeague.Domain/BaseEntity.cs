namespace FootballLeague.Domain;

public abstract class BaseEntity<T>
    where T : struct
{
    public T Id { get; set; }
}
