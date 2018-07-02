using P02_VillainNames;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace P05_ChangeTownNamesCasing
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string countryName = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                int countryId = GetCountryId(countryName,connection);
                if (countryId==0)
                {
                    Console.WriteLine("No town names were affected.");
                }
                else
                {
                    int affectedTowns = UpdateName(countryId, connection);
                    List<string> towns = GetTownNames(countryId, connection);
                    Console.WriteLine($"{affectedTowns} town names were affected. ");
                    Console.WriteLine($"[{String.Join(", ",towns)}]");
                }


                connection.Close();
            }
        }

        private static List<string> GetTownNames(int countryId, SqlConnection connection)
        {
            var names = new List<string>(); 
            string namesSql = "SELECT Name FROM Towns WHERE CountryCode=@countryId";
            using (SqlCommand command = new SqlCommand(namesSql, connection))
            {
                command.Parameters.AddWithValue("@countryId", countryId);
                using (SqlDataReader reader =command.ExecuteReader() )
                {
                    while (reader.Read())
                    {
                        names.Add(reader[0].ToString());
                    }
                }
            }
            return names;
        }

        private static int UpdateName(int countryId, SqlConnection connection)
        {
            string updateSql = "UPDATE Towns SET Name = UPPER(Name) WHERE CountryCode = @countryId";
            using (SqlCommand command = new SqlCommand(updateSql, connection))
            {
                command.Parameters.AddWithValue("@countryId", countryId);
                return command.ExecuteNonQuery();
            }
        }

        private static int GetCountryId(string countryName, SqlConnection connection)
        {
            string countryInfo = "SELECT TOP(1) c.Id FROM Towns AS t JOIN Countries AS c ON c.Id=t.CountryCode WHERE c.Name=@name";
            using (SqlCommand command = new SqlCommand(countryInfo,connection))
            {
                command.Parameters.AddWithValue("@name", countryName);
                if (command.ExecuteScalar()==null)
                {
                    return 0;
                }
                return (int)command.ExecuteScalar();
            }
        }
    }
}
