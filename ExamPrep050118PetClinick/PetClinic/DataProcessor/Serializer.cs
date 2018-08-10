namespace PetClinic.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.DataProcessor.Dtos.Export;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animalsByOwner = context.Animals
               .Where(x => x.Passport.OwnerPhoneNumber == phoneNumber)
               .OrderBy(x => x.Age)
               .ThenBy(x => x.PassportSerialNumber)
               .Select(x => new
               {
                   OwnerName = x.Passport.OwnerName,
                   AnimalName = x.Name,
                   Age = x.Age,
                   SerialNumber =x.PassportSerialNumber,
                   RegisteredOn =x.Passport.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
               })
               .ToArray();

            string result = JsonConvert.SerializeObject(animalsByOwner, Newtonsoft.Json.Formatting.Indented);
            return result;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var sb = new StringBuilder();
            var xmlProcedures = context.Procedures
                .Include(x => x.Animal)
                .ThenInclude(x => x.Passport)
                .Include(x => x.ProcedureAnimalAids)
                .ThenInclude(x => x.AnimalAid)
                .OrderBy(x => x.DateTime)
                .ThenBy(x => x.Animal.PassportSerialNumber)
                .Select(x => new ProcedureDto
                {
                    Passport=x.Animal.PassportSerialNumber,
                    OwnerNumber = x.Animal.Passport.OwnerPhoneNumber,
                    DateTime=x.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    AnimalAids = x.ProcedureAnimalAids
                        .Select(a=> new AnimalAidDto
                        {
                            Name=a.AnimalAid.Name,
                            Price=a.AnimalAid.Price.ToString("F2")
                        })
                        .ToArray(),
                    TotalPrice=x.ProcedureAnimalAids.Sum(z=>z.AnimalAid.Price).ToString("F2")
                })
                .ToArray();

            var xmlNamspaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var ser = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));
            ser.Serialize(new StringWriter(sb), xmlProcedures, xmlNamspaces);

            var result = sb.ToString().TrimEnd();
            return result;
        }
    }
}
