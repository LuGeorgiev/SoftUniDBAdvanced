namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.DataProcessor.Dtos.Import;
    using PetClinic.Models;

    public class Deserializer
    {
        private const string FailourMessage = "Error: Invalid data.";
        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            string successImport = "Record {0} successfully imported.";
            var sb = new StringBuilder();
            var deserializedAnimalAids = JsonConvert.DeserializeObject<AnimalAidDto[]>(jsonString);
            var validAnimalAids = new List<AnimalAid>();

            foreach (var animalAidDto in deserializedAnimalAids)
            {
                if (!IsValid(animalAidDto))
                {
                    sb.AppendLine(FailourMessage);
                    continue;
                }
                if (validAnimalAids.Any(x=>x.Name==animalAidDto.Name))
                {
                    sb.AppendLine(FailourMessage);
                    continue;
                }

                var animalAid = Mapper.Map<AnimalAid>(animalAidDto);
                sb.AppendLine(string.Format(successImport, animalAidDto.Name));
                validAnimalAids.Add(animalAid);
            }
            context.AnimalAids.AddRange(validAnimalAids);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            string successImport = "{0} Passport №: {1} successfully imported.";
            var sb = new StringBuilder();
            var deserializedAnimals = JsonConvert.DeserializeObject<AnimalDto[]>(jsonString);
            var validAnimals = new List<Animal>();
            var validPassports = new List<Passport>();

            foreach (var animalDto in deserializedAnimals)
            {
                if (!IsValid(animalDto))
                {
                    sb.AppendLine(FailourMessage);
                    continue;
                }
                if (!IsValid(animalDto.Passport))
                {
                    sb.AppendLine(FailourMessage);
                    continue;
                }
                if (validAnimals.Any(x=>x.Passport.SerialNumber==animalDto.Passport.SerialNumber))
                {
                    sb.AppendLine(FailourMessage);
                    continue;
                }

                //var validPassport = Mapper.Map<Passport>(animalDto.Passport);
                //validPassports.Add(validPassport);

                var validAnimal = Mapper.Map<Animal>(animalDto);
                //validAnimal.Passport = validPassport;
                validAnimals.Add(validAnimal);
                sb.AppendLine(string.Format(successImport, animalDto.Name, animalDto.Passport.SerialNumber));
            }
            context.Animals.AddRange(validAnimals);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            string successImport = "Record {0} successfully imported.";
            var sb = new StringBuilder();
            var ser = new XmlSerializer(typeof(VetDto[]), new XmlRootAttribute("Vets"));
            var deserializedVets = (VetDto[])ser.Deserialize(new StringReader(xmlString)); 
            var validVets = new List<Vet>();

            foreach (var vetDto in deserializedVets)
            {
                if (!IsValid(vetDto))
                {
                    sb.AppendLine(FailourMessage);
                    continue;
                }
                if (validVets.Any(x=>x.PhoneNumber==vetDto.PhoneNumber))
                {
                    sb.AppendLine(FailourMessage);
                    continue;
                }

                var vet = Mapper.Map<Vet>(vetDto);
                validVets.Add(vet);
                sb.AppendLine(string.Format(successImport, vetDto.Name));
            }
            context.Vets.AddRange(validVets);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            string successImport = "Record successfully imported.";

            var sb = new StringBuilder();
            var ser = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));
            var deserializedProcedures = (ProcedureDto[])ser.Deserialize(new StringReader(xmlString));

            var validProcedures = new List<Procedure>();
            var vets = context.Vets.ToArray();
            var animals = context.Animals.ToArray();
            var animalAids = context.AnimalAids.ToArray();

            foreach (var dtoProc in deserializedProcedures)
            {
                var vet = vets.SingleOrDefault(x => x.Name == dtoProc.Vet);
                var animal = animals.SingleOrDefault(x => x.PassportSerialNumber == dtoProc.Animal);
                var date = DateTime.ParseExact(dtoProc.DateTime,"dd-MM-yyyy",CultureInfo.InvariantCulture);

                if (vet==null||animal==null||!IsValid(dtoProc))
                {
                    sb.AppendLine(FailourMessage);
                    continue;
                }


                
                bool invalidAidDetected = false;
                var validProcedureAnimalAids = new List<ProcedureAnimalAid>();
                foreach (var aidDto in dtoProc.AnimalAids)
                {
                    var animalAid = animalAids.SingleOrDefault(x => x.Name == aidDto.Name);
                    if (animalAid==null||IsValid(aidDto))
                    {
                        sb.AppendLine(FailourMessage);
                        invalidAidDetected = true;
                        break;
                    }

                    if (validProcedureAnimalAids.Any(x=>x.AnimalAid.Name==aidDto.Name))
                    {
                        sb.AppendLine(FailourMessage);
                        invalidAidDetected = true;
                        break;
                    }
                    validProcedureAnimalAids.Add(new ProcedureAnimalAid { AnimalAid = animalAid });                  
                }
                if (invalidAidDetected)
                {
                    sb.AppendLine(FailourMessage);
                    continue;
                }
                
                var procedure = new Procedure()
                {
                     AnimalId=animal.Id,
                     VetId=vet.Id,
                     DateTime=date,
                     ProcedureAnimalAids=validProcedureAnimalAids
                };

                if (!IsValid(procedure))
                {
                    sb.AppendLine(FailourMessage);
                    continue;
                }
                validProcedures.Add(procedure);
                sb.AppendLine(successImport);
            }
            context.Procedures.AddRange(validProcedures);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        private static bool IsValid(object obj)
        {
            var validContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validResults = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validContext, validResults, true);
            return result;
        }
    }
}
