using BusTicketsSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketsSystem.Client.Core.Contracts
{
    interface ICommand
    {
        string Execute(BusTicketsSystemContext db, string[] data);
    }
}
