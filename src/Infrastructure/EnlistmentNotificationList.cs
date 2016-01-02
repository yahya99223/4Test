using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Infrastructure
{
    public class EnlistmentNotificationList<T> : IList<T>, IEnlistmentNotification
    {
        private bool isFirstOperation;
        private List<T> list;
        private List<T> oldList;


        public EnlistmentNotificationList(List<T> data = null)
        {
            this.oldList = new List<T>();
            list = data ?? new List<T>();
            isFirstOperation = true;
        }


        private void prepareTransaction()
        {
            var currentTx = Transaction.Current;
            if (currentTx != null)
            {
                currentTx.EnlistVolatile(this, EnlistmentOptions.None);
            }
            if (isFirstOperation)
            {
                oldList = list.Select(x => x.Copy()).ToList();
                isFirstOperation = false;
            }
        }

        #region Implementation of IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            prepareTransaction();
            return list.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
        #region Implementation of ICollection<T>

        public void Add(T item)
        {
            prepareTransaction();
            list.Add(item);
        }


        public void Clear()
        {
            prepareTransaction();
            list.Clear();
        }


        public bool Contains(T item)
        {
            return list.Contains(item);
        }


        public void CopyTo(T[] array, int arrayIndex)
        {
            prepareTransaction();
            list.CopyTo(array, arrayIndex);
        }


        public bool Remove(T item)
        {
            prepareTransaction();
            return list.Remove(item);
        }


        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly { get; }

        #endregion


        #region Implementation of IList<T>

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }


        public void Insert(int index, T item)
        {
            prepareTransaction();
            list.Insert(index, item);
        }


        public void RemoveAt(int index)
        {
            prepareTransaction();
            list.RemoveAt(index);
        }


        public T this[int index]
        {
            get
            {
                prepareTransaction();
                return list[index];
            }
            set
            {
                prepareTransaction();
                list[index] = value;
            }
        }

        #endregion


        #region Implementation of IEnlistmentNotification

        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            Console.WriteLine("EnlistmentNotificationList: Prepare");
            preparingEnlistment.Prepared();
        }


        public void Commit(Enlistment enlistment)
        {
            Console.WriteLine("EnlistmentNotificationList: Commit");
            oldList = new List<T>();
            isFirstOperation = true;
        }


        public void Rollback(Enlistment enlistment)
        {
            if (!isFirstOperation)
            {
                Console.WriteLine("EnlistmentNotificationList: Rollback");
                list = oldList.Select(x => x).ToList();
                oldList = new List<T>();
                isFirstOperation = true;
            }
        }


        public void InDoubt(Enlistment enlistment)
        {
            Console.WriteLine("EnlistmentNotificationList: InDoubt");
        }

        #endregion        
    }
}