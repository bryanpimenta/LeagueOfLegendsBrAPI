using LeagueOfLegendsBrAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DotEnvGoogle;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        DotEnv.Load();

        var connectionString = string.Format(
        Configuration.GetConnectionString("LeagueOfLegendsDatabase"),
        Environment.GetEnvironmentVariable("MYSQL_ROOT_PASSWORD"));

        services.AddDbContext<LeagueOfLegendsContext>(options =>
            options.UseMySQL(connectionString));

        services.AddDbContext<LeagueOfLegendsContext>(options =>
        {
            var connectionString = $"Server=db;Database=LeagueOfLegendsDataBase;User=root;Password={Environment.GetEnvironmentVariable("MYSQL_ROOT_PASSWORD")};";
            options.UseMySQL(connectionString);
        });


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
        }
        );

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
