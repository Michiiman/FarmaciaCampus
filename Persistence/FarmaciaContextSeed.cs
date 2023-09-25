using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Microsoft.Extensions.Logging;
namespace Persistence;

public class FarmaciaContextSeed
{
    public static async Task SeedAsync(FarmaciaContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            var ruta = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!context.Personas.Any())
            {
                using (var reader = new StreamReader(ruta + @"\Data\Csv/Persona.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Persona>();
                        List<Persona> entidad = new List<Persona>();
                        foreach (var item in list)
                        {
                            entidad.Add(new Persona
                            {
                                Id = item.Id,
                                Nombre = item.Nombre,
                                NumeroDocumento = item.NumeroDocumento,
                                TipoPersonaIdFk = item.TipoPersonaIdFk,
                                TipoDeDocumentoIdFk = item.TipoDeDocumentoIdFk,
                                Direccion=item.Direccion
                            });
                        }

                        context.Personas.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }

                }
            }if (!context.TiposPersonas.Any())
            {
                using (var readerTipoPersonas = new StreamReader(ruta + @"/Data/Csv/TipoPersona.csv"))
                {
                    using (var csv = new CsvReader(readerTipoPersonas, CultureInfo.InvariantCulture))
                    {
                        var list = csv.GetRecords<TipoPersona>();
                        context.TiposPersonas.AddRange(list);
                        await context.SaveChangesAsync();
                    }
                }
            }if (!context.TiposDocumentos.Any())
            {
                using (var readerTipoDocumento = new StreamReader(ruta + @"/Data/Csv/TipoDocumento.csv"))
                {
                    using (var csv = new CsvReader(readerTipoDocumento, CultureInfo.InvariantCulture))
                    {
                        var list = csv.GetRecords<TipoDocumento>();
                        context.TiposDocumentos.AddRange(list);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<FarmaciaContext>();
            logger.LogError(ex.Message);
        }
    }
}

