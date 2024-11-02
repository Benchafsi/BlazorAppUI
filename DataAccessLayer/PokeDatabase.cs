using BlazorAppUI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppUI.DataAccessLayer;

public class PokeDatabase : DbContext
{
    public PokeDatabase(DbContextOptions<PokeDatabase> options) : base(options)
    {
    }
    public DbSet<Pokemon> Pokemons { get; set; }
}
