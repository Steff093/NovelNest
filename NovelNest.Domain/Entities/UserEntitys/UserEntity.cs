using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.UI.Domain.Entities.LoginEntities
{
    public class UserEntity
    {
        [Key]
        public int UserID { get; set; }

        [MaxLength(50), Required]
        public string UserName { get; set; } = string.Empty;

        [MaxLength(255), Required]
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        [EmailAddress, Required]
        public string EmailAddress { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
    }
}
