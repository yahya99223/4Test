using System;
using System.Collections.Generic;

namespace Message.Contracts
{
    public interface IShowNotificationCommand
    {
        Guid OrderId { get; }
        IList<string> Notification { get; }
    }

    public class ShowNotificationCommand : IShowNotificationCommand
    {
        public ShowNotificationCommand(Guid orderId, IList<string> notification)
        {
            OrderId = orderId;
            Notification = notification;
        }

        public Guid OrderId { get; }
        public IList<string> Notification { get; }
    }
}