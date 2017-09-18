using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DistributeMe.ImageProcessing.WPF.Helpers;

namespace DistributeMe.ImageProcessing.WPF.ViewModels
{
    public class ProcessRequest : ObservableObject
    {
        private ObservableSetCollection<string> notifications;
        private Guid requestId;

        public ProcessRequest()
        {
            notifications = new ObservableSetCollection<string>();
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

        public ObservableSetCollection<string> Notifications
        {
            get { return notifications; }
            set
            {
                notifications = value;
                RaisePropertyChanged("Notifications");
            }
        }
    }
}