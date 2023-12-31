﻿using NovelNest.ApplicationLogic.Interfaces.IAddBookFeature;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Infrastructure.Interfaces;

namespace NovelNest.ApplicationLogic.Features.BookFeatures.AddBookFeature
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
                throw new Exception("Error");
            }
        }
    }
}
