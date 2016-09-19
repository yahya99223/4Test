using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DistributeMe.ImageProcessing.Messaging;
using DistributeMe.ImageProcessing.WPF.ViewModels;

namespace DistributeMe.ImageProcessing.WPF.Consumers
{
    public class OcrImageProcessedConsumer
    {
        private static readonly object locker = new object();

        public void Consume(IOcrImageProcessedEvent registeredEvent, ObservableCollection<ProcessRequest> processRequests)
        {
            var request = processRequests.FirstOrDefault(r => r.RequestId == registeredEvent.RequestId);
            if (request == null)
                return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                lock (locker)
                {
                    request.Notifications.Insert(0, $"OCR Result: {registeredEvent.ExtractedText}");
                }
            });
        }
    }
}