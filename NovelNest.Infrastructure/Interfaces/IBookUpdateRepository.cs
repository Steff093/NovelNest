using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Infrastructure.Interfaces
{
    public interface IBookUpdateRepository<T> where T : class
    {
        Task<T> UpdateBookAsync(T book);
    }
}
