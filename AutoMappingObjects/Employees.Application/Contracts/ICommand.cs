using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Application.Contracts
{
    internal interface ICommand
    {
        string Execute(params string[] args);
    }
}
