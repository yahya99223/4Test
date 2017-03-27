using System;
using System.Collections.Generic;
using System.Transactions;
using Core.Model;


namespace Core.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool isDisposed;
        public static IList<User> Users = new EnlistmentNotificationList<User>();
        //private TransactionScope transaction;
        private static object lck = new object();
        private bool rolledBack;


        public UnitOfWork()
        {
            StaticInfo.StartedUnitOfWorks += 1;
            this.isDisposed = false;
            //transaction = new TransactionScope();
        }


        public void Commit()
        {
            if (isDisposed)
            {
                StaticInfo.Exception = "UnitOfWork-Commit: already disposed";
                rolledBack = true;
                if (Users.Count > 0)
                    Users.RemoveAt(0);
                //throw new Exception("UnitOfWork disposed already");
            }
            else
            {
                StaticInfo.CommitedUnitOfWorks += 1;
            }
        }


        public void AddUser(User user)
        {
            Users.Add(user);
        }


        public void Dispose()
        {
            //Console.WriteLine("Disposing UOW");
            if (isDisposed)
            {
                StaticInfo.Exception = "UnitOfWork-Dispose: already disposed";
            }
            else
            {
                lock (lck)
                {

                    if (!rolledBack)
                        //transaction.Complete();

                    //transaction.Dispose();

                    StaticInfo.DisposedUnitOfWorks += 1;
                    //throw new Exception("UnitOfWork disposed already");
                    isDisposed = true;
                }
            }
        }
    }
}