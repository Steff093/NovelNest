using NovelNest.UI.Domain.Entities.LoginEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Infrastructure.Interfaces.IRegistrationInterfaceInfrastructure
{
    public interface IRegistrationRepository
    {
        Task RegistrationNewUser(UserEntity user);
    }
}
