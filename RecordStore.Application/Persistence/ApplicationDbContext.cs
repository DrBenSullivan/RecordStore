using Microsoft.EntityFrameworkCore;
using RecordStore.Core.Models;

namespace RecordStore.Application.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Album>(entity =>
            {
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Title).IsRequired().HasMaxLength(255);
                entity.Property(a => a.ReleaseYear).IsRequired();
                entity.Property(a => a.ArtistId).IsRequired();
                entity.HasOne(a => a.Artist).WithMany(ar => ar.Albums).HasForeignKey(a => a.ArtistId).OnDelete(DeleteBehavior.Cascade); 
                entity.Property(a => a.GenreId).IsRequired(false);
                entity.HasOne(a => a.Genre).WithMany().HasForeignKey(a => a.GenreId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(255);
            });
        }
    }
}
