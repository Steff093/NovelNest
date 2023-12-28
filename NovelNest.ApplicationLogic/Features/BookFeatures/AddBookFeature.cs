using NovelNest.ApplicationLogic.Interfaces;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Features.BookFeatures
{
    public class AddBookFeature : IAddBookFeature<BookEntity>
    {
        private readonly IBookAddRepository<BookEntity> _bookAddRepository;

        public AddBookFeature(IBookAddRepository<BookEntity> bookAddRepository)
        {
            _bookAddRepository = bookAddRepository;
        }

        public async Task<BookEntity> AddBookAsync(BookEntity book)
        {
            try
            {
                await _bookAddRepository.AddBookAsync(book);
                return book;
            }
            catch (Exception)
            {
                throw new Exception("Es gab einen Fehler!");
            }
        }
    }
}
