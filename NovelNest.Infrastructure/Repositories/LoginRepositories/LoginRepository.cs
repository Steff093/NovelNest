using Microsoft.EntityFrameworkCore;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces.LoginInterfaceInfrastructure;
using NovelNest.UI.Domain.Entities.LoginEntities;

namespace NovelNest.Infrastructure.Repositories.LoginRepositories
{
    public class LoginRepository : ILoginInterface
    {
        private readonly NovelNestDataContext _context;

        public LoginRepository(NovelNestDataContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> LoginSuccesful(string username)
        {
            return await _context.UserEntities.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
