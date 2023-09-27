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
            if (!context.TiposPersonas.Any())
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
            }
            if (!context.TiposDocumentos.Any())
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
                                TipoDocumentoIdFk = item.TipoDocumentoIdFk,
                                Direccion = item.Direccion
                            });
                        }

                        context.Personas.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }

                }
            }
            if (!context.Medicamentos.Any())
            {
                using (var reader = new StreamReader(ruta + @"\Data\Csv/Medicamento.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Medicamento>();
                        List<Medicamento> entidad = new List<Medicamento>();
                        foreach (var item in list)
                        {
                            entidad.Add(new Medicamento
                            {
                                Id = item.Id,
                                Nombre = item.Nombre,
                                Precio = item.Precio,
                                Stock = item.Stock,
                                FechaExpiracion = item.FechaExpiracion,
                                TipoMedicamento = item.TipoMedicamento,
                                ProveedorIdFk = item.ProveedorIdFk
                            });
                        }

                        context.Medicamentos.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }

                }
            }
            if (!context.Compras.Any())
            {
                using (var reader = new StreamReader(ruta + @"\Data\Csv/Compra.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Compra>();
                        List<Compra> entidad = new List<Compra>();
                        foreach (var item in list)
                        {
                            entidad.Add(new Compra
                            {
                                Id = item.Id,
                                FechaCompra = item.FechaCompra,
                                ProveedorIdFk = item.ProveedorIdFk
                                
                            });
                        }

                        context.Compras.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }

                }
            }
            if (!context.MedicamentosComprados.Any())
            {
                using (var reader = new StreamReader(ruta + @"\Data\Csv/MedicamentoComprado.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<MedicamentoComprado>();
                        List<MedicamentoComprado> entidad = new List<MedicamentoComprado>();
                        foreach (var item in list)
                        {
                            entidad.Add(new MedicamentoComprado
                            {
                                Id = item.Id,
                                CompraIdFk = item.CompraIdFk,
                                MedicamentoIdFk = item.MedicamentoIdFk,
                                CantidadComprada = item.CantidadComprada,
                                PrecioCompra = item.PrecioCompra
                                
                            });
                        }

                        context.MedicamentosComprados.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }

                }
            }
            /*if (!context.Recetas.Any())
            {
                using (var reader = new StreamReader(ruta + @"\Data\Csv/Receta.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Receta>();
                        List<Receta> entidad = new List<Receta>();
                        foreach (var item in list)
                        {
                            entidad.Add(new Receta
                            {
                                Id = item.Id,
                                FechaExpedicion = item.FechaExpedicion,
                                PacienteIdFk = item.PacienteIdFk,
                                DoctorIdFk = item.DoctorIdFk,
                                Descripcion = item.Descripcion
                            });
                        }

                        context.Recetas.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }

                }
            }*/
        }

        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<FarmaciaContext>();
            logger.LogError(ex.Message);
        }
    }
}

