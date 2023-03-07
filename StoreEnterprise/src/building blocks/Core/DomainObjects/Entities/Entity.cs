using Core.Messages;

namespace Core.DomainObjects.Entities;

public abstract class Entity
{
    public Guid Id { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    private List<Event> _notifications;

    public IReadOnlyCollection<Event> Notifications 
        => _notifications.AsReadOnly();

    public void AddEvent(Event eventItem)
        => _notifications.Add(eventItem);
    
    public void RemoveEvent(Event eventItem)
        => _notifications.Remove(eventItem);    
    
    public void ClearEvents()
        => _notifications.Clear();    
    
    public override bool Equals(object obj)
    {
        if (obj is not Entity compareTo) return false;
        if (ReferenceEquals(this, compareTo)) return true;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

        return a.Equals(b);
    }
    
    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return GetType().GetHashCode() * 907 + Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }
    
    
}