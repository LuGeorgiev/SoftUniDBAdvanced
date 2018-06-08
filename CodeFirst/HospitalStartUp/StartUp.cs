using System;
using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;

namespace HospitalStartUp
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new HospitalContext())
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
