using NovelNest.ApplicationLogic.Features.BookFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Services.BookService.AddBookService
{
    public class AddBookService
    {
        private readonly AddBookFeature _addbookFeature;

        public AddBookService(AddBookFeature addBookFeature)
        {
            _addbookFeature = addBookFeature;
        }
    }
}
