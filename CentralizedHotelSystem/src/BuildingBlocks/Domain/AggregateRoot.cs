namespace BuildingBlocks.Domain;

public abstract class AggregateRoot
{
    private readonly List<DomainEvent> _events = new();
    public IReadOnlyCollection<DomainEvent> Events => _events.AsReadOnly();

    protected void AddEvent(DomainEvent @event) => _events.Add(@event);
    public void ClearEvents() => _events.Clear();
}
