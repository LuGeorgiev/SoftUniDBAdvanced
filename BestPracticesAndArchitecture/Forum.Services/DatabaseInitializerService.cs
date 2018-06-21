using System;
using System.Collections.Generic;
using System.Text;
using Forum.Data;
using Forum.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Forum.Services
{

    public class DatabaseInitializerService : IDbInitializerService
    {
        private ForumDbContext context;
        public DatabaseInitializerService(ForumDbContext context)
        {
            this.context = context;
        }

        public void InitializeDatabase()
        {
            context.Database.Migrate();
        }
    }
}
