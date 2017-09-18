using System.Collections.ObjectModel;

namespace DistributeMe.ImageProcessing.WPF.Helpers
{
    public class ObservableSetCollection<T> : ObservableCollection<T>
    {
        public void Append(T item)
        {
            if (Contains(item))
                return;
            base.Add(item);
        }

        protected override void InsertItem(int index, T item)
        {
            if (Contains(item))
                return;
            base.InsertItem(index, item);
        }
    }
}