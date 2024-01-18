using NovelNest.Domain.Entities.BookEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Infrastructure.Interfaces.BookInterfaceInfrastructure.DeleteBookInterfaceInfrastructure
{
    public interface IBookDeleteRepository
    {
        Task DeleteBookAsync(BookEntity book);
    }
}
