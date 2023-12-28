using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Infrastructure.Interfaces
{
    public interface IBookAddRepository<T> where T : class
    {
        Task AddBookAsync(T book);
    }
}
