using Microsoft.EntityFrameworkCore.Internal;
using NovelNest.ApplicationLogic.Interfaces.RegistrationInterface;
using NovelNest.Infrastructure.Interfaces.IRegistrationInterfaceInfrastructure;
using NovelNest.UI.Domain.Entities.LoginEntities;

namespace NovelNest.ApplicationLogic.Features.RegistrationFeatures
{
    public class RegistrationUserFeature : IRegistrationFeatures
    {
        private readonly IRegistrationRepository _registeredServices;

        public RegistrationUserFeature(IRegistrationRepository registrationInterface)
        {
            _registeredServices = registrationInterface;
        }

        public async Task AddUserToTable(UserEntity entity)
        {
            try
            {
                await _registeredServices.RegistrationNewUser(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<UserEntity> GetUserByUsername(string username)
        {
            try
            {
                return await _registeredServices.GetUserByUsername(username);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
