using P02_VillainNames;
using System;
using System.Data.SqlClient;

namespace P04_AddMinion
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string[] mimionInfo = Console.ReadLine().Split();
            string[] villanInfo = Console.ReadLine().Split();

            string minionName = mimionInfo[1];
            int minionAge = int.Parse(mimionInfo[2]);
            string townName = mimionInfo[3];

            string villainName = villanInfo[1];

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                int townId = GetTownId(townName,connection);
                int villainId = GetVillanId(villainName, connection);
                int minionId = InsertMinionAndGetId(minionName, minionAge, townId,connection);
                AssignMinionToVillian(villainId, minionId, connection);

                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}");
                connection.Close();
            }
        }

        private static void AssignMinionToVillian(int villainId, int minionId, SqlConnection connection)
        {
            string insertMinionVillan = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";
            using (SqlCommand command = new SqlCommand(insertMinionVillan,connection))
            {
                command.Parameters.AddWithValue("@minionId", minionId);
                command.Parameters.AddWithValue("@villainId", villainId);
                command.ExecuteNonQuery();
            }
        }

        private static int InsertMinionAndGetId(string minionName, int minionAge, int townId, SqlConnection connection)
        {
            string insertMinion =  "INSERT INTO Minions (Name, Age,TownId) VALUES(@name, @age, @townId)";
            using (SqlCommand command = new SqlCommand(insertMinion,connection))
            {
                command.Parameters.AddWithValue("@name", minionName);
                command.Parameters.AddWithValue("@age", minionAge);
                command.Parameters.AddWithValue("@townId", townId);
                command.ExecuteNonQuery();
            }

            string minionsSql = "SELECT Id FROM Minions WHERE Name=@name";
            using (SqlCommand command = new SqlCommand(minionsSql, connection))
            {
                command.Parameters.AddWithValue("@name", minionName);                

                return (int)command.ExecuteScalar();
            }
        }

        private static int GetVillanId(string villanName, SqlConnection connection)
        {
            string villainSql = "SELECT Id FROM Villains WHERE Name=@Name";
            using (SqlCommand command = new SqlCommand(villainSql, connection))
            {
                command.Parameters.AddWithValue("@Name", villanName);
                if (command.ExecuteScalar() == null)
                {
                    InsertIntoVillains(villanName, connection);
                    Console.WriteLine($"Villain {villanName} was added to the database.");
                }
                return (int)command.ExecuteScalar();
            }
        }

        private static void InsertIntoVillains(string villanName, SqlConnection connection)
        {
            string insertvillains = "INSERT INTO Villains (Name) VALUES (@villainName)";
            using (SqlCommand command = new SqlCommand(insertvillains, connection))
            {
                command.Parameters.AddWithValue("@villainName", villanName);
                command.ExecuteNonQuery();
            }
        }

        private static int GetTownId(string townName, SqlConnection connection)
        {
            string townSql = "SELECT Id FROM Towns WHERE Name=@Name";
            using (SqlCommand command = new SqlCommand(townSql,connection))
            {
                command.Parameters.AddWithValue("@Name", townName);
                if (command.ExecuteScalar()==null)
                {
                    InsertIntoTowns(townName, connection);
                    Console.WriteLine($"Town {townName} was added to the database.");
                }
                return (int)command.ExecuteScalar();
            }
        }

        private static void InsertIntoTowns(string townName, SqlConnection connection)
        {
            string insertTown = "INSERT INTO Towns (Name) VALUES (@townName)";
            using (SqlCommand command = new SqlCommand(insertTown, connection))
            {
                command.Parameters.AddWithValue("@townName", townName);
                command.ExecuteNonQuery();
            }
        }
    }
}
