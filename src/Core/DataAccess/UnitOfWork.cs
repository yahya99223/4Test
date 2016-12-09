using System;
using System.Collections.Generic;
using System.Transactions;
using Core.Model;


namespace Core.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool isDisposed;
        private static IList<User> users = new EnlistmentNotificationList<User>();
        private TransactionScope transaction;


        public UnitOfWork()
        {
            StaticInfo.StartedUnitOfWorks += 1;
            this.isDisposed = false;
            transaction = new TransactionScope();
        }


        public void Commit()
        {
            if (isDisposed)
            {
                StaticInfo.Exception = "UnitOfWork-Commit: already disposed";
                //throw new Exception("UnitOfWork disposed already");
            }
            else
            {
                StaticInfo.CommitedUnitOfWorks += 1;
            }
        }


        public void AddUser(User user)
        {
            users.Add(user);
            StaticInfo.Users += 1;
        }


        public void Dispose()
        {
            if (isDisposed)
            {
                StaticInfo.Exception = "UnitOfWork-Dispose: already disposed";
            }
            else
            {
                StaticInfo.DisposedUnitOfWorks += 1;
                //throw new Exception("UnitOfWork disposed already");
                isDisposed = true;
            }
        }
    }
}