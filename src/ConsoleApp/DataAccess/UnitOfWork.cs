using System;
using System.Transactions;

namespace ConsoleApp.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private readonly TransactionScope transaction;


        public UnitOfWork()
        {
            transaction = new TransactionScope();
        }


        public UserRepository UserRepository
        {
            get { return new UserRepository(); }
        }

        public DoctorRepository DoctorRepository
        {
            get { return new DoctorRepository(); }
        }


        #region Implementation of IDisposable

        public void Dispose()
        {
            transaction.Dispose();
        }

        #endregion


        public void Commit()
        {
            transaction.Complete();
            Dispose();
        }
    }
}