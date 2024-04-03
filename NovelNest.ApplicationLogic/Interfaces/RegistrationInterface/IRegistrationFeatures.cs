using NovelNest.UI.Domain.Entities.LoginEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Interfaces.RegistrationInterface
{
    public interface IRegistrationFeatures
    {
        Task AddUserToTable(UserEntity entity);            
    }
}
