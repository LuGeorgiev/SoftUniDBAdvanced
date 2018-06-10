using System;
using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Initializer;
using P01_HospitalDatabase.Data.Models;

namespace HospitalStartUp
{
    class StartUp
    {
        static void Main()
        {
            DatabaseInitializer.ResetDatabase();

            //using (var db = new HospitalContext())
            //{
            //        // Fisrts task 66/100
            //    db.Database.EnsureCreated();
            //    
            //}
        }
    }
}
