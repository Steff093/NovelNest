using NovelNest.ApplicationLogic.Interfaces.BookInterfaces.IDeleteBookFeature;
using NovelNest.ApplicationLogic.Interfaces.FolderInterfaces.FolderDelete;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Domain.Entities.FolderEntities;
using NovelNest.Infrastructure.Interfaces.FolderInterfaceInfrastructure.FolderDeleteInterfaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Features.FolderFeature.FolderDeleteFeature
{
    public class DeleteFolderFeatures : IFolderDeleteFeatures
    {
        private readonly IFolderDelete _folderDelete;

        public DeleteFolderFeatures(IFolderDelete folderDelete)
        {
            _folderDelete = folderDelete;
        }

        public async Task DeleteFolder(FolderEntity folder)
        {
            await _folderDelete.DeleteFolderAsync(folder);
        }
    }
}
