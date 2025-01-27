using RecordStore.Api.Middleware;
using RecordStore.Application.Services;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Infrastructure.Extensions;
using RecordStore.Infrastructure.Repositories;

namespace RecordStore.Api
{
	public class Program
	{
		public async static Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDatabase(builder.Configuration, builder.Environment);

			if (builder.Environment.IsDevelopment())
			{
				await builder.Services.SeedDbAsync();
				builder.Services.AddCors(options =>
				{
					options.AddPolicy("AllowLocalhost",
						policy => policy.WithOrigins("http://localhost:5191")
										.AllowAnyMethod()
										.AllowAnyHeader());
				});
			}

			builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
			builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
			builder.Services.AddScoped<IGenreRepository, GenreRepository>();

			builder.Services.AddScoped<IAlbumService, AlbumService>();
			builder.Services.AddScoped<IArtistService, ArtistService>();
			builder.Services.AddScoped<IGenreService, GenreService>();

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddTransient<ErrorHandlingMiddleware>();


			var app = builder.Build();

			app.UseMiddleware<ErrorHandlingMiddleware>();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
				app.UseCors("AllowLocalHost");
			}

			app.UseStatusCodePagesWithRedirects("/Error/{0}");

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
