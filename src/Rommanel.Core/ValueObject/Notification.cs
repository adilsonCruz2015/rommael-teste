

using Rommanel.Core.Interfaces;

namespace Rommanel.Core.ValueObject
{
    public class Notification : INotification
    {
        private readonly List<Messages> _messages;
        public Notification()
        {
            _messages = new List<Messages>();
        }

        public void Handle(Messages messages)
        {
            _messages.Add(messages);
        }

        public List<Messages> GetNotifications()
        {
            return _messages;
        }

        public bool HasNotification()
        {
            return _messages.Any(x => x.MessageType.Equals(MessageType.Error));
        }

        public bool IsValid()
        {
            return !HasMessages(MessageType.Error);
        }

        private bool HasMessages(params MessageType[] types)
        {
            return _messages.Where(x => types.Contains(x.MessageType)).Take(1).Count().Equals(1);
        }

        public NotificationType? GetHighestPriorityError()
        {
            return _messages.FirstOrDefault()?.NotificationType;
        }
    }
}
