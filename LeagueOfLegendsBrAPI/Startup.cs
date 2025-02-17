using LeagueOfLegendsBrAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        string? connectionString = Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("A string de conexão 'DefaultConnection' não está configurada.");
        }

        services.AddDbContext<LeagueOfLegendsContext>(options =>
            options.UseMySQL(connectionString));

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "LeagueOfLegendsBrAPI", Version = "v1" });
        });

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "LeagueOfLegendsBrAPI");
        });

        app.UseCors("AllowAllOrigins");

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
