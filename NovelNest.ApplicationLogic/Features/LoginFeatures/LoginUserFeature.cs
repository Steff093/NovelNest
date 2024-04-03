using NovelNest.ApplicationLogic.Interfaces.LoginInterface;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Interfaces.LoginInterfaceInfrastructure;
using NovelNest.UI.Domain.Entities.LoginEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Features.LoginFeatures
{
    public class LoginUserFeature : ILoginFeatures
    {
        private readonly ILoginInterface _loginInterface;

        public LoginUserFeature(ILoginInterface loginInterface)
        {
            _loginInterface = loginInterface;
        }

        public async Task<UserEntity> LoginSucces(string username)
        {
            try
            {
                return await _loginInterface.LoginSuccesful(username);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
