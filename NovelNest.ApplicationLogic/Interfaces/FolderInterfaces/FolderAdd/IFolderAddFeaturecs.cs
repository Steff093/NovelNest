using NovelNest.Domain.Entities.FolderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Interfaces.FolderInterfaces.FolderAdd
{
    public interface IFolderAddFeaturecs
    {
        Task AddFolder(FolderEntity folderEntity);
    }
}
