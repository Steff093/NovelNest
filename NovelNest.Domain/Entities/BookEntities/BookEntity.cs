using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Domain.Entities.BookEntities
{
    public class BookEntity
    {
        [Key]
        public int BookId { get; set; }

        [Required, MinLength(5)]
        public string Title { get; set; } = string.Empty;

        [Required, MinLength(25)]
        public string Description { get; set; } = string.Empty;
    }
}
