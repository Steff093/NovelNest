using NovelNest.Domain.Entities.FolderEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces.FolderInterfaceInfrastructure.FolderDeleteInterfaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Infrastructure.Repositories.FolderRepositories.FolderDeleteRepositories
{
    public class FolderDeleteRepository : IFolderDelete
    {
        private readonly NovelNestDataContext _context;

        public FolderDeleteRepository(NovelNestDataContext context)
        {
            _context = context;
        }

        public async Task DeleteFolderAsync(FolderEntity folder)
        {
            _context.FolderEntities.Remove(folder);
            await _context.SaveChangesAsync();
        }
    }
}
