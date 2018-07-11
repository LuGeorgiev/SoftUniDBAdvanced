using BusTicketsSystem.Client.Core;
using BusTicketsSystem.Client.Core.Contracts;
using BusTicketsSystem.Client.Utility;
using BusTicketsSystem.Data;

namespace BusTicketsSystem.Client
{
    class StartUp
    {
        static void Main(string[] args)
        {
            //RestartDatabase();
            //InitialDbSeed();

            IParseCommand commandParser = new CommandParser();
            Engine engine = new Engine(commandParser);

            engine.Run();
        }

        private static void InitialDbSeed()
        {
            using (var db = new BusTicketsSystemContext())
            {
                var initialSeed = new InitialDbSeed();
                initialSeed.Seed(db);
            }             
        }

        private static void RestartDatabase()
        {
            using (var db= new BusTicketsSystemContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
