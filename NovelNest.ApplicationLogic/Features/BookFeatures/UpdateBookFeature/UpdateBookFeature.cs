using NovelNest.ApplicationLogic.Interfaces;
using NovelNest.Domain.Entities.BookEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.ApplicationLogic.Features.BookFeatures.UpdateBookFeature
{
    public class UpdateBookFeature : IUpdateBookFeature<BookEntity>
    {
        private readonly IUpdateBookFeature<BookEntity> _updateBookFeature;

        public UpdateBookFeature(IUpdateBookFeature<BookEntity> updateBookFeature)
        {
            _updateBookFeature = updateBookFeature;
        }

        public async Task<BookEntity> UpdateAsync(BookEntity book)
        {

            return await _updateBookFeature.UpdateAsync(book);
        }
    }
}
