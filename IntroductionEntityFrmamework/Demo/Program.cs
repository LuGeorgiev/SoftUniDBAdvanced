using Demo.Data;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new WMSDbContext())
            {
                var clients = context
                    .Client
                    .Include(c=>c.FirstName)
                    .ToList();


                var mechanic = new Mechanic() { FirstName = "Ivan", LastName = "Ivanov", Address = "Ivanqne" };
                context.Mechanics.Add(mechanic);

                var jobs = context.Jobs                    
                    .Select(x=> new
                    {
                     x.Mechanic.FirstName,                 
                     x.Orders.Count
                    })
                    .ToList();

                foreach (var job in jobs)
                {
                    Console.WriteLine($"{job.FirstName} blah blah {job.Count}");
                }
            }
            }
    }
}
