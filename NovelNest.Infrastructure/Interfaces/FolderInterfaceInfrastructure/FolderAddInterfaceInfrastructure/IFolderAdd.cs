using NovelNest.Domain.Entities.FolderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Infrastructure.Interfaces.FolderInterfaceInfrastructure.FolderAddInterfaceInfrastructure
{
    public interface IFolderAdd
    {
        Task AddFolderAsync(FolderEntity entity);
    }
}
