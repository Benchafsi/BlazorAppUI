using BlazorAppUI.Components;
using BlazorAppUI.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppUI;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services
            .AddDbContext<PokeDatabase>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Register the seeder as a scoped service.
        builder.Services.AddScoped<DatabaseSeeder>();
        var app = builder.Build();

        // Seed the database during startup.
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var seeder = services.GetRequiredService<DatabaseSeeder>();
            seeder.SeedData("C:\\Users\\Vergil\\Desktop\\PokeApi\\BlazorAppUI\\Assets\\PokeDex\\pokedex.csv");
        }
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
