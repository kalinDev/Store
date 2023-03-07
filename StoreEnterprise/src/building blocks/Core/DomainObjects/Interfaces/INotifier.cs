using Core.DomainObjects.Entities;

namespace Core.DomainObjects.Interfaces;

public interface INotifier
{
    void Handle(Notification notification);
    List<Notification> GetNotifications();
    bool HasNotification();
}