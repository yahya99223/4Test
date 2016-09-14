using System;
using Helpers;

namespace ServiceTire
{
    public class FakeService : BaseService<FakeService>, IFakeService
    {
        public FakeService(string fake) : base(fake)
        {
        }

        public void DoGoodWork()
        {
            try
            {
                Console.WriteLine("Good Job");
                this.Log().Info("========> Bad Work logged.");
            }
            catch (Exception ex)
            {
                this.Log().Error(ex.ToString());
            }
        }

        public void DoBadWork()
        {
            try
            {
                Console.WriteLine("Bad Work");
                this.Log().Warn("========> Bad Work logged.");
            }
            catch (Exception ex)
            {
                this.Log().Error(ex.ToString());
            }
        }

        public void DoError()
        {
            try
            {
                Console.WriteLine("Error!!! Work");
                throw new Exception("You did some error!.", new ArgumentException("This is your mistake. You did it intentionally!.", new AccessViolationException("We're just kidding ^_^")));
            }
            catch (Exception ex)
            {
                this.Log().Error(ex.GetDetailedMessage(), ex);
            }
        }
    }
}