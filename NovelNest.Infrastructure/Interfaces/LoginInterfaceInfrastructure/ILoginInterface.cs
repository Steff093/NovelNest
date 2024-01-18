using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Infrastructure.Interfaces.LoginInterfaceInfrastructure
{
    public interface ILoginInterface
    {
        void LoginSuccesful(string username, string password);
    }
}
