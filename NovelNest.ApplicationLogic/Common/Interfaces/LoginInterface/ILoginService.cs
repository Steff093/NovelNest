using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Common.Interfaces.LoginInterface
{
    public interface ILoginService
    {
        bool Authenticate(string username, string password);
    }
}
