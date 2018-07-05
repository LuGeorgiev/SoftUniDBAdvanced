using P02_VillainNames;
using System;
using System.Data.SqlClient;

namespace P06_RemoveVillain
{
    class Program
    {
        static void Main(string[] args)
        {
            int infoVillainId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                int villianId = GetVillain(infoVillainId, connection, transaction);
                if (villianId==0)
                {
                    Console.WriteLine("No such villain was found.");
                }
                else
                {
                    try
                    {
                        int affectedRows = ReleaseMinions(villianId, connection, transaction);
                        string villainName = GetVillainName(villianId, connection, transaction);
                        DeleteVillain(villianId, connection, transaction);

                        Console.WriteLine($"{villainName} was deleted.");
                        Console.WriteLine($"{affectedRows} minions were released.");
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e.Message);
                        transaction.Rollback();
                    }
                    
                }

                connection.Close();
            }
        }

        private static void DeleteVillain(int villianId, SqlConnection connection, SqlTransaction transaction)
        {
            string name = "DELETE FROM Villains WHERE Id=@Id";
            using (var command = new SqlCommand(name, connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", villianId);
                command.ExecuteNonQuery();
            }
        }

        private static string GetVillainName(int villianId, SqlConnection connection, SqlTransaction transaction)
        {
            string name = "SELECT Name FROM Villains WHERE Id=@Id";
            using (var command = new SqlCommand(name, connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", villianId);
                return (string)command.ExecuteScalar();
            }
        }

        private static int ReleaseMinions(int villianId, SqlConnection connection, SqlTransaction transaction)
        {
            string villianToRelease = "DELETE FROM MinionsVillains WHERE VillainId=@Id";
            using (var command = new SqlCommand(villianToRelease, connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", villianId);
                return command.ExecuteNonQuery();
            }
        }

        private static int GetVillain(int infoVillainId, SqlConnection connection, SqlTransaction transaction)
        {
            string villianInfo = "SELECT * FROM Villains WHERE Id=@Id";
            using (SqlCommand command = new SqlCommand(villianInfo,connection,transaction))
            {
                command.Parameters.AddWithValue("@Id" , infoVillainId);
               
                if (command.ExecuteScalar() == null)
                {
                    return 0;
                }
                return (int)command.ExecuteScalar();
            }
        }
    }
}
