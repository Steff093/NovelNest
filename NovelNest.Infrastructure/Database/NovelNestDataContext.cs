using Microsoft.EntityFrameworkCore;
using NovelNest.Domain.Entities.BookEntities;

namespace NovelNest.Infrastructure.Database
{
    public class NovelNestDataContext : DbContext
    {
        public NovelNestDataContext()
        {
        }

        public NovelNestDataContext(DbContextOptions<NovelNestDataContext> options) { }
        public DbSet<BookEntity> BookEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
