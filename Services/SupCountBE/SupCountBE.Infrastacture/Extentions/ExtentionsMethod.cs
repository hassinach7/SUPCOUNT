using System.Reflection;
using System.Text.Json;

namespace SupCountBE.Infrastacture.Extentions;

public static class ExtentionsMethod
{
    public static ModelBuilder InitiData(this ModelBuilder builder)
    {
        string filePath = Path.Combine("Data", "SeedData", "Categories.json");
        string? assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if(assemblyLocation is null)
        {
            throw new InvalidOperationException("Unable to get assembly location.");
        }
        string fullPath = Path.Combine(assemblyLocation, filePath);
        string? jsonData = File.ReadAllText(fullPath);
        if (string.IsNullOrEmpty(jsonData))
        {
            throw new InvalidOperationException("Unable to read JSON data.");
        }
        List<Category>? categories = JsonSerializer.Deserialize<List<Category>>(jsonData);
        if (categories is null)
        {
            throw new InvalidOperationException("Unable to deserialize JSON data.");
        }
        foreach (var category in categories)
        {
            builder.Entity<Category>().HasData(category);
        }
        return builder;
    }
}
