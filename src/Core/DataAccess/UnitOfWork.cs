using System;

namespace Core.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool isDisposed;

        public UnitOfWork()
        {
            StaticInfo.StartedUnitOfWorks += 1;
            this.isDisposed = false;
        }

        public void Commit()
        {
            if (isDisposed)
                throw new Exception("UnitOfWork disposed already");

            StaticInfo.CommitedUnitOfWorks += 1;
        }

        public void Dispose()
        {
            StaticInfo.DisposedUnitOfWorks += 1;
            if (isDisposed)
                throw new Exception("UnitOfWork disposed already");

            isDisposed = true;
        }
    }
}