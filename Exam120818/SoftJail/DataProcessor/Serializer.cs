namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class Serializer
    {
       

        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisioners = context.Prisoners
                .Where(x => ids.Contains(x.Id))
                .OrderBy(x=>x.FullName)
                .ThenBy(x=>x.Id)
                .Select(x=>new
                {
                    Id=x.Id,
                    Name=x.FullName,
                    CellNumber=x.Cell.CellNumber,
                    Officers = x.PrisonerOfficers
                    .Where(o=>o.PrisonerId==x.Id)
                    .OrderBy(o=>o.Officer.FullName)
                    .ThenBy(o=>o.Officer.Id)
                    .Select(o=>new
                    {
                        OfficerName=o.Officer.FullName,
                        Department=o.Officer.Department.Name
                    })
                    
                    .ToArray(),
                    TotalOfficerSalary=Math.Round(x.PrisonerOfficers.Sum(o=>o.Officer.Salary),2)
                })
                .ToArray();

            var result = JsonConvert.SerializeObject(prisioners, Newtonsoft.Json.Formatting.Indented);
            return result;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var sb = new StringBuilder();
            var names = prisonersNames.Split(',').ToArray();

            var prisoners = new PrisonersDto
            {  Prisoners= context.Prisoners
                .Where(x => names.Contains(x.FullName))
                .OrderBy(x => x.FullName)
                .ThenBy(x => x.Id)
                .Select(x => new PrisonerExportDto
                    {
                        Id = x.Id,
                        Name = x.FullName,
                        IncarcerationDate = x.IncarcerationDate.ToString("yyyy-MM-dd"),
                        EncryptedMessages = x.Mails.Select(m => new MessageDto
                        {
                            Description = m.Description
                    })
                        .ToArray()

                })
                .ToArray()
            };

            foreach (var prisoner in prisoners.Prisoners)
            {
                foreach (var msg in prisoner.EncryptedMessages)
                {
                    string newDescription = "";
                    for (int i = msg.Description.Length-1; i >= 0; i--)
                    {
                        newDescription += msg.Description[i];
                    }

                    msg.Description = newDescription.Trim();
                }
            } 


            var xmlNamspaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var ser = new XmlSerializer(typeof(PrisonersDto), new XmlRootAttribute("Prisoners"));
            ser.Serialize(new StringWriter(sb), prisoners, xmlNamspaces);

            var result = sb.ToString().TrimEnd();
            return result;
        }
    }
}