using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.Data;

namespace TeamBuilder.App.Contracts
{
    public interface ICommand
    {
        string Execute(TeamBuilderContext db, string[] args);
    }
}
