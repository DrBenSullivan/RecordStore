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
        public DbSet<AlbumStock> AlbumStock { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(artist => artist.Id).ValueGeneratedOnAdd();
                entity.HasKey(artist => artist.Id);
                entity.Property(artist => artist.Name).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(genre => genre.Id).ValueGeneratedOnAdd();
                entity.HasKey(genre => genre.Id);
                entity.Property(genre => genre.Name).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.Property(album => album.Id).ValueGeneratedOnAdd();
                entity.HasKey(album => album.Id);
                entity.Property(album => album.Title).IsRequired().HasMaxLength(255);
                entity.Property(album => album.ReleaseYear).IsRequired();
                entity.Property(album => album.ArtistId).IsRequired();
                entity.HasOne(album => album.Artist).WithMany().HasForeignKey(album => album.ArtistId).OnDelete(DeleteBehavior.Cascade);
                entity.Property(album => album.GenreId).IsRequired(false);
                entity.HasOne(album => album.Genre).WithMany().HasForeignKey(album => album.GenreId).OnDelete(DeleteBehavior.SetNull);
                entity.HasIndex(a => new { a.Title, a.ReleaseYear, a.ArtistId }).IsUnique();
            });

            modelBuilder.Entity<AlbumStock>(entity =>
            {
                entity.Property(stock => stock.AlbumId).IsRequired();
                entity.HasKey(stock => stock.AlbumId);
                entity.HasOne(stock => stock.Album).WithOne(album => album.Stock).HasForeignKey<AlbumStock>(stock => stock.AlbumId).OnDelete(DeleteBehavior.Restrict);
                entity.Property(stock => stock.Quantity).HasDefaultValue(0);
            });
        }

        internal async Task Seed()
        {
            var serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            string basePath = "../RecordStore.Infrastructure/Resources";

            var genresJson = File.ReadAllText(Path.Combine(basePath, "GenreSeedData.json"));
            var genres = JsonSerializer.Deserialize<List<Genre>>(genresJson, serializerOptions);
            if (genres == null || !genres.Any()) throw new Exception("Genres data is null or empty.");
            await Genres.AddRangeAsync(genres);
            await SaveChangesAsync();

            var artistsJson = File.ReadAllText(Path.Combine(basePath, "ArtistSeedData.json"));
            var artists = JsonSerializer.Deserialize<List<Artist>>(artistsJson, serializerOptions);
            if (artists == null || !artists.Any()) throw new Exception("Artists data is null or empty.");
            await Artists.AddRangeAsync(artists);
            await SaveChangesAsync();

            var albumsJson = File.ReadAllText(Path.Combine(basePath, "AlbumSeedData.json"));
            var albums = JsonSerializer.Deserialize<List<Album>>(albumsJson, serializerOptions);
            if (albums == null || !albums.Any()) throw new Exception("Albums data is null or empty.");
            await Albums.AddRangeAsync(albums);
            await SaveChangesAsync();

            var stockJson = File.ReadAllText(Path.Combine(basePath, "AlbumStockData.json"));
            var stocks = JsonSerializer.Deserialize<List<AlbumStock>>(stockJson, serializerOptions);
            if (stocks == null || !stocks.Any()) throw new Exception("Album Stocks data is null or empty");
            await AlbumStock.AddRangeAsync(stocks);
            await SaveChangesAsync();
        }
    }
}
