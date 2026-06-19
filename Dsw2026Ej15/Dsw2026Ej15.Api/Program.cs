using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data;

namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();

            builder.Services.AddHealthChecks();
            var app = builder.Build();
    
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); 
                    c.RoutePrefix = string.Empty; 
                });
            }
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/health-check");
            app.Run();
        }
    }
}
