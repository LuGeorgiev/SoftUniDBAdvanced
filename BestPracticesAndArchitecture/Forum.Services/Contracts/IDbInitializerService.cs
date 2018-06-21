using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Services.Contracts
{
    public interface IDbInitializerService
    {
        void InitializeDatabase();
    }
}
