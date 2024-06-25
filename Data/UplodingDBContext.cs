using Microsoft.EntityFrameworkCore;
using ArchiveUploud.Models;

namespace ArchiveUploud.Data
{
    public class UplodingDBContext : DbContext
    {
        public UplodingDBContext(DbContextOptions<UplodingDBContext> options) : base(options)
        {
        }

        public DbSet<FileRecord> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileRecord>()
                .HasOne(f => f.Folder)
                .WithMany(f => f.Files)
                .HasForeignKey(f => f.FolderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Folder>()
                .HasMany(f => f.SubFolders)
                .WithOne(f => f.ParentFolder)
                .HasForeignKey(f => f.ParentFolderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}