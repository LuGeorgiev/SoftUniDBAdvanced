using P02_VillainNames;
using System;
using System.Data.SqlClient;

namespace P03_MinionNames
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                int villanId = int.Parse(Console.ReadLine());
                string villanName = GetVillanName(villanId, connection);
                if (villanName==null)
                {
                    Console.WriteLine($"No villain with ID {villanId} exists in the database.");
                }
                else
                {
                    Console.WriteLine($"Villain: {villanName}");
                    string minionNames = MinionNames(villanId, connection);
                    Console.WriteLine(minionNames);
                }
                
                connection.Close();
            }
        }

        private static string MinionNames(int villanId, SqlConnection connection)
        {
            string minions = $"SELECT Name, Age FROM Minions AS m JOIN MinionsVillains AS mv ON mv.MinionId=m.Id WHERE mv.VillainId=@Id";
            string names = "";
            using (SqlCommand command = new SqlCommand(minions,connection))
            {
                command.Parameters.AddWithValue("Id", villanId);
                using (SqlDataReader reader =command.ExecuteReader() )
                {
                    if (!reader.HasRows)
                    {
                        names = "(no minions)";
                    }
                    else
                    {
                        int row = 1;
                        while (reader.Read())
                        {
                            names += $"{row++}. {reader[0]} {reader[1]}{Environment.NewLine}";
                        }
                    }
                }
                
            }
            return names.TrimEnd();
        }

        private static string GetVillanName(int villanId, SqlConnection connection)
        {
            string nameSql = $"SELECT Name FROM Villains WHERE Id=@id";

            using (SqlCommand command = new SqlCommand(nameSql,connection))
            {
                command.Parameters.AddWithValue("@id", villanId);
                return (string)command.ExecuteScalar();
            }
        }
    }
}
