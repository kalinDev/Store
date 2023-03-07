namespace Core.DomainObjects.Entities;

public class Notification
{
    public Notification(string message)
        => Message = message;

    public string Message { get; }
}