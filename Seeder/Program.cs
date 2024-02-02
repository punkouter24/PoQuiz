using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PoQuiz;
using PoQuiz.Models;
using System.Globalization;

string relativePath = @"../../../../PoQuiz/PoQuiz.db";
string fullPath = Path.GetFullPath(relativePath);
Console.WriteLine("Full path to the database file: " + fullPath);

if (File.Exists(fullPath))
{
    Console.WriteLine("The database file exists.");
}
else
{
    Console.WriteLine("The database file does not exist at the specified path.");
}

Console.WriteLine("Current working directory: " + Directory.GetCurrentDirectory());

ServiceProvider serviceProvider = new ServiceCollection()
    .AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=" + fullPath))
    .BuildServiceProvider();

ApplicationDbContext? context = serviceProvider.GetService<ApplicationDbContext>();

SeedData(context, "nbaallstargames.csv");

static void SeedData(ApplicationDbContext dbContext, string filePath)
{
    if (dbContext.NbaAllStarGames.Any())
    {
        Console.WriteLine("The database is already seeded.");
        return;
    }

    using (StreamReader reader = new(filePath))
    using (CsvReader csv = new(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        IgnoreBlankLines = true,
        MissingFieldFound = null, // Ignore missing fields
        HeaderValidated = null, // Ignore header validation issues
    }))


    //{
    //    // csv.Context.RegisterClassMap<NbaAllStarGameMap>(); // Register the class map if you use one
    //    List<NbaAllStarGame> records = csv.GetRecords<NbaAllStarGame>().ToList();
    //    dbContext.NbaAllStarGames.AddRange(records);
    //    _ = dbContext.SaveChanges();
    //}



    {
        List<NbaAllStarGame> records = new();
        _ = csv.Read();
        _ = csv.ReadHeader();
        while (csv.Read())
        {
            NbaAllStarGame record = new()
            {
                Player = csv.GetField("Player"),
                Year = csv.GetField<int>("Year"),
                TeamAbbreviation = csv.GetField<string>("Team") ?? "DefaultTeam", // Provide a default value if null
                Role = csv.GetField("Role"),
                MVP = csv.GetField("MVP"),
            };
            records.Add(record);
        }

        //dbContext.NbaAllStarGames.AddRange(records.Where(r => !string.IsNullOrEmpty(r.Team)));
        dbContext.NbaAllStarGames.AddRange(records);
        _ = dbContext.SaveChanges();
    }




    Console.WriteLine("Data seeding completed successfully.");
}







