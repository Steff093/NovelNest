﻿using Microsoft.EntityFrameworkCore;
using NovelNest.Domain.Entities.BookEntities;

namespace NovelNest.Infrastructure.Database
{
    public class NovelNestDataContext : DbContext
    {
        public NovelNestDataContext(DbContextOptions<NovelNestDataContext> options) { }
        public DbSet<BookEntity> BookEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=GISELA\\SQLEXPRESS;Initial Catalog=NovelNest;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
