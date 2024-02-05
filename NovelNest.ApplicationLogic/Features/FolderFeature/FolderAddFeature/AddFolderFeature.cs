using NovelNest.ApplicationLogic.Interfaces.FolderInterfaces.FolderAdd;
using NovelNest.Domain.Entities.FolderEntities;
using NovelNest.Infrastructure.Interfaces.FolderInterfaceInfrastructure.FolderAddInterfaceInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace NovelNest.ApplicationLogic.Features.FolderFeature.FolderAddFeature
{
    public class AddFolderFeature : IFolderAddFeaturecs
    {
        private readonly IFolderAdd _folderAdd;

        public AddFolderFeature(IFolderAdd folderAdd)                
        {
            _folderAdd = folderAdd;
        }

        public async Task AddFolder(FolderEntity folderEntity)
        {
            try
            {
                await _folderAdd.AddFolderAsync(folderEntity);
            }
            catch
            {
                throw new Exception("Error");
            }
        }
    }
}
