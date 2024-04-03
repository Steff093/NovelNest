using NovelNest.UI.Domain.Entities.LoginEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Interfaces.LoginInterface
{
    public interface ILoginFeatures
    {
        Task<UserEntity> LoginSucces(string username);
    }
}
