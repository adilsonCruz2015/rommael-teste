using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Rommanel.Core.ValueObject;
using Notifiy = Rommanel.Core.Interfaces;

namespace Rommanel.Api.Controllers.Common
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly Notifiy.INotification _notification;
        protected readonly IMediator _mediator;

        /// <summary>
        /// MainController
        /// </summary>
        /// <param name="notification"></param>
        public MainController(Notifiy.INotification notification,
                              IMediator mediator)
        {
            _notification = notification;
            _mediator = mediator;
        }

        /// <summary>
        /// OperationValid
        /// </summary>
        /// <returns></returns>
        protected bool OperationValid()
        {
            return !_notification.HasNotification();
        }

        /// <summary>
        /// CustomResponse
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyErrorModelStateInvalid(modelState);
            return CustomResponse();
        }

        /// <summary>
        /// CustomResponse
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperationValid())
            {
                if (!Equals(result, null))
                    return SuccessWithParameters(result);
                else
                    SuccessWithoutParameters();
            }

            return Failure();
        }

        /// <summary>
        /// Retorna Ok
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult SuccessWithParameters(object result)
        {
            return Ok(new
            {
                success = true,
                response = result,
                notifications = _notification.GetNotifications().Where(m => m.MessageType.Equals(MessageType.Success)).Select(x => x.Message)
            });
        }

        protected ActionResult SuccessWithoutParameters()
        {
            return Ok(new
            {
                success = true,
                notifications = _notification.GetNotifications().Where(m => m.MessageType.Equals(MessageType.Success)).Select(x => x.Message)
            });
        }

        protected ActionResult Failure()
        {
            var notificationType = _notification.GetHighestPriorityError() ?? NotificationType.BadRequest;
            var notifications = _notification.GetNotifications().Where(m => m.MessageType.Equals(MessageType.Error));

            return notificationType switch
            {
                NotificationType.NotFound => NotFound(new { success = false, notifications }),
                NotificationType.Unauthorized => Unauthorized(new { success = false, notifications }),
                NotificationType.Forbidden => Forbid(),
                NotificationType.ServerError => StatusCode(500, new { success = false, notifications }),
                _ => BadRequest(new { success = false, notifications })
            };
        }

        /// <summary>
        /// NumberOfRecordsFound
        /// Returns the number of records in the object
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static int NumberOfRecordsFound(object result)
        {
            if (result is IEnumerable<object> resultList)
                return resultList.Count();

            return 0; // Retorna 0 se `result` for null ou não for uma coleção
        }

        /// <summary>
        /// NotifyErrorModelStateInvalid
        /// </summary>
        /// <param name="modelState"></param>
        protected void NotifyErrorModelStateInvalid(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        /// <summary>
        /// NotifyError
        /// </summary>
        /// <param name="message"></param>
        protected void NotifyError(string message)
        {
            _notification.Handle(new Messages(message));
        }
    }
}
