using Microsoft.EntityFrameworkCore;
using NovelNest.Domain.Entities.BookEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.Domain.Entities.FolderEntities
{
    public class FolderEntity
    {
        [Key]
        public int FolderID { get; set; }

        [MaxLength(50)]
        public string FolderName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string FolderDescription { get; set; } = string.Empty;

        public virtual ICollection<BookEntity> BookEntities { get; set; } = new List<BookEntity>();
    }
}
