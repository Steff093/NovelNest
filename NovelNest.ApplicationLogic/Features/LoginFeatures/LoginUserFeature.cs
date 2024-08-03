using NovelNest.ApplicationLogic.Interfaces.LoginInterface;
using NovelNest.Infrastructure.Interfaces.LoginInterfaceInfrastructure;
using NovelNest.UI.Domain.Entities.LoginEntities;

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
