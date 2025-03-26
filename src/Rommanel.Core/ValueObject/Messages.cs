
namespace Rommanel.Core.ValueObject
{
    public class Messages
    {
        public Messages()
        { }

        public Messages(string message)
            : this(message, MessageType.Error) { }

        public Messages(string message, Exception exception)
            : this(message, MessageType.Error)
        {
            Exception = exception;
        }

        public Messages(string message, MessageType messageType)
            : this()
        {
            Message = message;
            MessageType = messageType;
        }

        public Messages(string message, MessageType messageType, NotificationType notificationType)
            : this(message, messageType)
        {
            NotificationType = notificationType;
        }

        public string Message { get; private set; }

        public MessageType MessageType { get; private set; }

        public Exception Exception { get; private set; }

        public NotificationType NotificationType { get; private set; }
    }
}
