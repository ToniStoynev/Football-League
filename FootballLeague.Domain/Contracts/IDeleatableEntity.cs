namespace FootballLeague.Domain.Contracts;

internal interface IDeleatableEntity
{
    bool IsDeleted { get; set; }
}
