using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OrderManagement.ViewModel
{
    public class ProcessRequest : ObservableObject
    {
        private ObservableCollection<string> notifications;
        private Guid requestId;
        private string notificationString;

        public ProcessRequest()
        {
            notifications = new ObservableCollection<string>();
        }

        public Guid RequestId
        {
            get { return requestId; }
            set
            {
                requestId = value;
                RaisePropertyChanged("RequestId");
            }
        }

        public string NotificationString
        {
            get { return notificationString; }
            set
            {
                notificationString = value;
                RaisePropertyChanged("NotificationString");
                Notifications = value.Split('&');
            }
        }

        public IEnumerable<string> Notifications
        {
            get { return notifications; }
            set
            {
                notifications = new ObservableCollection<string>(value);
                RaisePropertyChanged("Notifications");
            }
        }
    }
}