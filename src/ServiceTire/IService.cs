using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTire
{
    public interface IFakeService
    {
        void DoGoodWork();
        void DoBadWork();
        void DoError();
    }
}
