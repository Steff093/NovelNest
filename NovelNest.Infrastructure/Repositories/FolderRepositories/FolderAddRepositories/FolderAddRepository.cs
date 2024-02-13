using NovelNest.Domain.Entities.FolderEntities;
using NovelNest.Infrastructure.Database;
using NovelNest.Infrastructure.Interfaces.FolderInterfaceInfrastructure.FolderAddInterfaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Infrastructure.Repositories.FolderRepositories.FolderAddRepositories
{
    public class FolderAddRepository : IFolderAdd
    {
        private readonly NovelNestDataContext _context;

        public FolderAddRepository(NovelNestDataContext context)
        {
            _context = context;
        }

        public async Task AddFolderAsync(FolderEntity folderEntity)
        {
            try
            {
                _context.FolderEntities.Add(folderEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception) { }
        }
    }
}
