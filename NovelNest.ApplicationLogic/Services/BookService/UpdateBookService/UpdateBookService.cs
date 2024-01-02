using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Services.BookService.UpdateBookService
{
    public class UpdateBookService
    {
        private readonly UpdateBookService _updateBookService;

        public UpdateBookService(UpdateBookService updateBookService)
        {
            _updateBookService = updateBookService;
        }
    }
}
