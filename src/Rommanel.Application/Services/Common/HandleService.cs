

using FluentValidation;
using Rommanel.Core.Interfaces;
using Rommanel.Core.ValueObject;
using FluentValidation.Results;

namespace Rommanel.Application.Services.Common
{
    public class HandleService
    {
        public HandleService(INotification notification)
        {
            _notification = notification;
        }

        private readonly INotification _notification;

        protected void Notify(string message)
        {
            _notification.Handle(new Messages(message));
        }

        protected void Notify(string message, MessageType messageType)
        {
            _notification.Handle(new Messages(message, messageType));
        }

        protected void Notify(string message, MessageType messageType, NotificationType notificationType)
        {
            _notification.Handle(new Messages(message, messageType, notificationType));
        }

        protected void Notify(string message, Exception exception)
        {
            _notification.Handle(new Messages(message, exception));
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                Notify(error.ErrorMessage);
        }

        protected bool RunValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE>
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }

        protected bool HasNotification()
        {
            return _notification.HasNotification();
        }

        protected bool IsValid()
        {
            return _notification.IsValid();
        }
    }
}
