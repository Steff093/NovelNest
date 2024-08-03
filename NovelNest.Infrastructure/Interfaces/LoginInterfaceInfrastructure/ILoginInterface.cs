using NovelNest.UI.Domain.Entities.LoginEntities;

namespace NovelNest.Infrastructure.Interfaces.LoginInterfaceInfrastructure
{
    public interface ILoginInterface
    {
        Task<UserEntity> LoginSuccesful(string username);
    }
}
