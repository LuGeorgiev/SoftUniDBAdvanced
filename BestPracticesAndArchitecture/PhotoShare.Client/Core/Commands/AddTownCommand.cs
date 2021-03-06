﻿namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System;
    using PhotoShare.Client.Core.Contracts;

    public class AddTownCommand:ICommand
    {
        // AddTown <townName> <countryName>
        public string Execute(string[] data)
        {
            string townName = data[1];
            string country = data[2];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                //P02.Extend Photo Share System refactoring
                if (Session.User == null)
                {
                    throw new InvalidOperationException("Invalid credentials! Please, Login.");
                }
               

                var townCheck = context.Towns
                    .AsNoTracking()  // only check and not 
                    .Where(t => t.Name == townName)
                    .FirstOrDefault();
                //if(context.Towns.Any(t=>t.Name==townName)) //another aproach
                if (townCheck!=null)
                {
                    throw new ArgumentException($"Town: {townName} already exists!");
                }

                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                context.Towns.Add(town);
                context.SaveChanges();

                return townName + " was added to database!";
            }
        }
    }
}
