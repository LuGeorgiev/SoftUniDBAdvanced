using System;


namespace TeamBuilder.App.Core
{
    public class Engine
    {


        public void Run()
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.GetBaseException().Message);
                }

            }
        }
    }
}
