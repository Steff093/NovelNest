using NovelNest.ApplicationLogic.Interfaces.IUpdateBookFeature;
using NovelNest.ApplicationLogic.Repositories;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Services.BookService
{
    public class BookService 
    {
        private readonly IUpdateBookFeature<BookEntity> _updateBookFeature;

        public BookService(IUpdateBookFeature<BookEntity> updateBookFeature)
        {
            _updateBookFeature = updateBookFeature;
        }

        public async Task<BookEntity> UpdateBookAsync(BookEntity book)
        {
            return await _updateBookFeature.UpdateBookAsync(book);
        }
    }
}
