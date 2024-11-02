using BlazorAppUI.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace BlazorAppUI.DataAccessLayer;

public class DatabaseSeeder
{
    private readonly PokeDatabase _context;

    public DatabaseSeeder(PokeDatabase context)
    {
        _context = context;
    }

    public void SeedData(string filePath)
    {
        if (!_context.Pokemons.Any())
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null, // Disable header validation to avoid exceptions
                MissingFieldFound = null // Ignore missing fields
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                // Map CSV headers to class properties if names differ
                csv.Context.RegisterClassMap<PokemonClassMap>();

                var pokemons = csv.GetRecords<Pokemon>().ToList();
                _context.Pokemons.AddRange(pokemons);
                _context.SaveChanges();
            }
        }
    }
}
public class PokemonClassMap : ClassMap<Pokemon>
{
    public PokemonClassMap()
    {
        Map(m => m.Image).Name("Image");
        Map(m => m.Index).Name("Index");
        Map(m => m.Name).Name("Name");
        Map(m => m.Type1).Name("Type 1");
        Map(m => m.Type2).Name("Type 2");
        Map(m => m.Total).Name("Total");
        Map(m => m.HP).Name("HP");
        Map(m => m.Attack).Name("Attack");
        Map(m => m.Defense).Name("Defense");
        Map(m => m.SPAtk).Name("SP. Atk.");
        Map(m => m.SPDef).Name("SP. Def");
        Map(m => m.Speed).Name("Speed");
    }
}