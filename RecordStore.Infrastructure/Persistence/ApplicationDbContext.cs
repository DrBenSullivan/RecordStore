using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RecordStore.Core.Models;

namespace RecordStore.Infrastructure.Persistence
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
                entity.HasOne(a => a.Artist).WithMany().HasForeignKey(a => a.ArtistId).OnDelete(DeleteBehavior.Cascade);
                entity.Property(a => a.GenreId).IsRequired(false);
                entity.HasOne(a => a.Genre).WithMany().HasForeignKey(a => a.GenreId).OnDelete(DeleteBehavior.SetNull);
                entity.HasIndex(a => new { a.Title, a.ReleaseYear, a.ArtistId }).IsUnique();
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

        internal void Seed()
        {
            var serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            string basePath = "../RecordStore.Infrastructure/Resources";

            var artistsJson = File.ReadAllText(Path.Combine(basePath, "ArtistSeedData.json"));
            var artists = JsonSerializer.Deserialize<List<Artist>>(artistsJson, serializerOptions);
            if (artists == null || !artists.Any())
            {
                throw new Exception("Artists data is null or empty.");
            }

            var genresJson = File.ReadAllText(Path.Combine(basePath, "GenreSeedData.json"));
            var genres = JsonSerializer.Deserialize<List<Genre>>(genresJson, serializerOptions);
            if (genres == null || !genres.Any())
            {
                throw new Exception("Genres data is null or empty.");
            }

            var albumsJson = File.ReadAllText(Path.Combine(basePath, "AlbumSeedData.json"));
            var albums = JsonSerializer.Deserialize<List<Album>>(albumsJson, serializerOptions);
            if (albums == null || !albums.Any())
            {
                throw new Exception("Albums data is null or empty.");
            }

            Genres.AddRange(genres);
            Albums.AddRange(albums);
            Artists.AddRange(artists);
        }
    }
}
