using Microsoft.EntityFrameworkCore;
using NovelNest.Domain.Entities.BookEntities;
using NovelNest.Domain.Entities.FolderEntities;
using NovelNest.UI.Domain.Entities.LoginEntities;

namespace NovelNest.Infrastructure.Database
{
    public class NovelNestDataContext : DbContext
    {
        public NovelNestDataContext()
        {

        }
        public NovelNestDataContext(DbContextOptions<NovelNestDataContext> options) { }

        public DbSet<BookEntity> BookEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<FolderEntity> FolderEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=GISELA\\SQLEXPRESS;Initial Catalog=NovelNest;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FolderEntity>()
                .HasMany(f => f.BookEntities)
                .WithOne(b => b.Folder)
                .HasForeignKey(b => b.FolderID);
        }
    }
}
