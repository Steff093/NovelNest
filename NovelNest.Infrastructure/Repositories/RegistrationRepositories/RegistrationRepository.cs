using Microsoft.EntityFrameworkCore;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces.IRegistrationInterfaceInfrastructure;
using NovelNest.UI.Domain.Entities.LoginEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Infrastructure.Repositories.RegistrationRepositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly NovelNestDataContext _context;

        public RegistrationRepository(NovelNestDataContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetUserByUsername(string username)
        {
            return await _context.UserEntities.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task RegistrationNewUser(UserEntity user)
        {
            try
            {
                _context.UserEntities.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
