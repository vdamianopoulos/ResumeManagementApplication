using DataPersistency.Abstractions;
using DataPersistency.DataContext;
using DataPersistency.Filters;
using DataPersistency.Repositories;
using DataPersistency.Services;
using Microsoft.EntityFrameworkCore;

namespace DataPersistency
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<InternalServerErrorFilter>();
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<ICandidateService, CandidateService>();
            builder.Services.AddTransient<ICandidateSpecificOperationsRepository, CandidateSpecificOperationsRepository>();
            builder.Services.AddTransient<IDegreeSpecificOperationsRepository, DegreeSpecificOperationsRepository>();
            builder.Services.AddTransient<IDegreeService, DegreeService>();
            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddTransient<IDatabase, SqliteDb>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowMyBlazorApp", builder =>
                {
                    builder.WithOrigins("https://localhost:7111", "http://localhost:5173")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddDbContext<ResumeContext>(options =>
                    options.UseSqlite(connectionString: "Data Source=resumes.db"), ServiceLifetime.Singleton);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var dbSetup = app.Services.GetService<IDatabase>();
            await dbSetup.SetupAsync();

            app.UseCors("AllowMyBlazorApp");

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
