using Rommanel.Core.ValueObject;

namespace Rommanel.Core.Interfaces
{
    public interface INotification
    {
        bool HasNotification();

        List<Messages> GetNotifications();

        void Handle(Messages messages);

        bool IsValid();

        NotificationType? GetHighestPriorityError();
    }
}
