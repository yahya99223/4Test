using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }


    public class UnitOfWork : IUnitOfWork
    {
        private bool isDisposed;

        public UnitOfWork()
        {
            StaticInfo.UnitOfWorks += 1;
            this.isDisposed = false;
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (isDisposed)
                throw new Exception("UnitOfWork disposed already");

            isDisposed = true;
        }
    }
}
