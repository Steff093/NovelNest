using NovelNest.Domain.Entities.FolderEntities;
using System.ComponentModel.DataAnnotations;

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

        [MaxLength(1024 * 1024 * 5)]
        public byte[]? ImageBook { get; set; }

        public string? ImageMIMEType { get; set; }

        public bool? IsPictureAvailable { get; set; }

        public int? FolderID { get; set; }

        public virtual FolderEntity Folder { get; set; }
    }
}
