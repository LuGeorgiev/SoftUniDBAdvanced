using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.Models;

namespace TeamBuilder.App.Contracts
{
    interface ICommandHelper
    {
        bool IsTeamExisting(string teamName);
        bool IsUserExisting(string username);
        bool IsInviteExisting(string teamName, User user);
        bool IsUserCreatorOfTeam(string teamName, User user);
        bool IsUserCreatorOfEvent(string eventName, User user);
        bool IsMemberOfTeam(string teamName, string username);
        bool IsEventExisting(string eventName);
    }
}
