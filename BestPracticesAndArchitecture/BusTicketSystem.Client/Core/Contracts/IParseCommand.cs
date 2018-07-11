using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketsSystem.Client.Core.Contracts
{
    public interface IParseCommand
    {
        string DispatchCommand(string comand,string[] data);
    }
}
