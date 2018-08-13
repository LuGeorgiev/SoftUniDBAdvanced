namespace SoftJail.DataProcessor
{
    using AutoMapper;
    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private static string InvalidEntry = "Invalid Data";
        private static string ImportDepartment = "Imported {0} with {1} cells";
        private static string ImportPrisoner = "Imported {0} {1} years old";
        private static string ImportOfficer = "Imported {0} ({1} prisoners)";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var deserializedDepartments = JsonConvert.DeserializeObject<DepartmentDto[]>(jsonString);
            var validDepartments = new List<Department>();

            foreach (var depDto in deserializedDepartments)
            {
                if (!IsValid(depDto))
                {
                    sb.AppendLine(InvalidEntry);
                    continue;
                }
                bool allCellsValid = true;
                var validCells = new List<Cell>();

                foreach (var cellDto in depDto.Cells)
                {
                    if (!IsValid(cellDto))
                    {
                        allCellsValid = false;
                        break;
                    }

                    var cell = Mapper.Map<Cell>(cellDto);
                    
                    validCells.Add(cell);
                }
                if (!allCellsValid)
                {
                    sb.AppendLine(InvalidEntry);
                    continue;
                }

                var department = Mapper.Map<Department>(depDto);                
                department.Cells = validCells;

                validDepartments.Add(department);
                sb.AppendLine(string.Format(ImportDepartment, depDto.Name, department.Cells.Count));
            }
            context.AddRange(validDepartments);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var deserializedProsoners = JsonConvert.DeserializeObject<PrisonerDto[]>(jsonString);
            var validPrisoners = new List<Prisoner>();

            foreach (var pdto in deserializedProsoners)
            {
                if (!IsValid(pdto))
                {
                    sb.AppendLine(InvalidEntry);
                    continue;
                }
                bool allMailsValid = true;
                var validMails = new List<Mail>();

                foreach (var mailDto in pdto.Mails)
                {
                    if (!IsValid(mailDto))
                    {
                        allMailsValid = false;
                        break;
                    }

                    //var mail = Mapper.Map<Mail>(mailDto);
                    var mail = new Mail
                    {
                         Description=mailDto.Description,
                         Sender=mailDto.Sender,
                         Address= mailDto.Address
                    };
                    validMails.Add(mail);
                }
                if (!allMailsValid)
                {
                    sb.AppendLine(InvalidEntry);
                    continue;
                }

                DateTime? releaseDate = null;
                if (pdto.ReleaseDate!=null)
                {
                   releaseDate = DateTime.ParseExact(pdto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                int? cellId = null;
                if (pdto.CellId!=null)
                {
                    cellId = pdto.CellId.Value;
                }

                decimal? bail = null;
                if (pdto.Bail != null)
                {
                    bail = pdto.Bail.Value;
                }


                //var prisoner = Mapper.Map<Prisoner>(pdto);
                var prisoner = new Prisoner
                {
                    FullName = pdto.FullName,
                    Nickname = pdto.Nickname,
                    Age = pdto.Age,
                    //Bail = pdto.Bail.Value,
                    //IncarcerationDate = DateTime.ParseExact(pdto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    //ReleaseDate = releaseDate.Value,
                    Mails=validMails,
                    //CellId=cellId.Value
                };

                prisoner.CellId = cellId;
                prisoner.Bail = pdto.Bail;
                prisoner.IncarcerationDate = DateTime.ParseExact(pdto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                prisoner.ReleaseDate = releaseDate;


                validPrisoners.Add(prisoner);
                sb.AppendLine(string.Format(ImportPrisoner, pdto.FullName, prisoner.Age));
            }
            context.Prisoners.AddRange(validPrisoners);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var ser = new XmlSerializer(typeof(OfficerDto[]), new XmlRootAttribute("Officers"));
            var deserializedPOfficers = (OfficerDto[])ser.Deserialize(new StringReader(xmlString));

            var validOfficerss = new List<Officer>();

            foreach (var offDto in deserializedPOfficers)
            {
                if (!IsValid(offDto))
                {
                    sb.AppendLine(InvalidEntry);
                    continue;
                }
                bool validPosition = Enum.TryParse<Position>(offDto.Position, out var position);
                bool validWeapon = Enum.TryParse<Weapon>(offDto.Weapon, out var weapon);

                //var department = context.Departments.FirstOrDefault(x => x.Id == offDto.DepartmentId);

                if (!validPosition||!validWeapon)
                {
                    sb.AppendLine(InvalidEntry);
                    continue;
                }

                
                var officer = new Officer
                {
                     FullName=offDto.FullName,
                     Salary=offDto.Salary,
                     DepartmentId=offDto.DepartmentId,
                     Position=position,
                     Weapon=weapon
                };
                
                foreach (var prisonDto in offDto.Priosoners)
                {
                    officer.OfficerPrisoners.Add(new OfficerPrisoner
                    {
                        PrisonerId = int.Parse(prisonDto.Id)
                    });
                }
                               

                validOfficerss.Add(officer);
                sb.AppendLine(string.Format(ImportOfficer, officer.FullName, officer.OfficerPrisoners.Count));

            }
            context.Officers.AddRange(validOfficerss);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        private static bool IsValid(object obj)
        {
            var validCon = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validResul = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validCon, validResul, true);

            return result;
        }
    }
}