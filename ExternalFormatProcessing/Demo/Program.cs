using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new
            {
                Name="Pesho",
                Age = 13,
                Grades = new[] 
                {
                    5.5,
                    4,
                    3.75,
                    6
                }
            };

            var outputJson = JsonConvert.SerializeObject(obj, Formatting.Indented);
            // new JsonSerializer options as THIRD option
            //Console.WriteLine(outputJson);

            var template = new
            {
                Name = default(string),
                Age = default(int),
                Grades = new decimal[]
                {
                }
            };

            var deserializedObj = JsonConvert.DeserializeAnonymousType(outputJson, template);
            ;

            // can be used as JS :)
            JObject json = JObject.Parse("{'Name':'Pesho', 'Age':15, 'RandomElements':[2.5, 'Glosho']}");

            foreach (var kvp in json)
            {
                var key = kvp.Key;
                var value = kvp.Value;

                Console.WriteLine($"{key} : {value}");
            }
        }
    }
}
