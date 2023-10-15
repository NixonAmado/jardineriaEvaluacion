using System.Globalization;
using CsvHelper;
using Microsoft.Extensions.Logging;
using Persistencia.Data;

namespace Persistence;

public class DbAppContextSeed
{
    // public static async Task SeedAsync(DbAppContext context, ILoggerFactory loggerFactory)
    // {
    //     try
    //     {
    //         /* var ruta = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); */

    //         if (!context.Users.Any())
    //         {
    //             /* Console.WriteLine("ruta:"+ruta); */
    //              using (var readerTypeFile = new StreamReader("../Persistence/Data/Csvs/Typefile.csv"))
    //             {
    //                 // using (var csv = new CsvReader(readerTypeFile, CultureInfo.InvariantCulture))
    //                 // {
    //                 //     var list = csv.GetRecords<TypeFile>();
    //                 //     context.TypeFiles.AddRange(list);
    //                 //     await context.SaveChangesAsync();
    //                 // }
    //             }
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         var logger = loggerFactory.CreateLogger<DbAppContext>();
    //         logger.LogError(ex.Message);
    //     }
    // }
}