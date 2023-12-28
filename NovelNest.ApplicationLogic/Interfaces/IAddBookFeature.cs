using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Interfaces
{
    public interface IAddBookFeature<T> where T : class
    {
        public Task<T> AddBookAsync(T book);
    }
}
