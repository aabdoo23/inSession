using Microsoft.EntityFrameworkCore;

namespace inSession
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=db15646.public.databaseasp.net; Database=db15646; User Id=db15646; Password=Fh4-5L+qK#i2; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policyBuilder =>
                {
                    // This lambda allows any origin. The key difference is that
                    // instead of AllowAnyOrigin(), we use SetIsOriginAllowed.
                    policyBuilder
                        .SetIsOriginAllowed(origin => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("MyPolicy");


            app.MapControllers();

            app.Run();
        }
    }
}
