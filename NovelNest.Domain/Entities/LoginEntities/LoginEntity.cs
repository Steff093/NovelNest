﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.UI.Domain.Entities.LoginEntities
{
    public class LoginEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50), Required]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255), Required]
        public string Password { get; set; } = string.Empty;
    }
}
