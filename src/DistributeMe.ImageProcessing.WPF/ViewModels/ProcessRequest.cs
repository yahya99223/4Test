using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DistributeMe.ImageProcessing.WPF.Helpers;

namespace DistributeMe.ImageProcessing.WPF.ViewModels
{
    public class ProcessRequest : ObservableObject
    {
        private ObservableCollection<string> notifications;
        private Guid requestId;

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

        public ObservableCollection<string> Notifications
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