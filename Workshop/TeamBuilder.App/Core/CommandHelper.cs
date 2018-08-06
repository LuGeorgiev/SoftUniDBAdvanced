using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Contracts;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core
{
    internal class CommandHelper : ICommandHelper
    {
        private readonly TeamBuilderContext context;
        public CommandHelper(TeamBuilderContext context)
        {
            this.context = context;
        }

        public bool IsEventExisting(string eventName)
        {
            return context.Events.Any(x => x.Name == eventName);

        }

        public bool IsInviteExisting(string teamName, User user)
        {
            return context.Invitaions
                .Any(i => i.Team.Name == teamName 
                        && i.InvitedUserId == user.Id 
                        && i.IsActive == true);
        }

        public bool IsMemberOfTeam(string teamName, string username)
        {
            return context.Teams
                .SingleOrDefault(t => t.Name == teamName)
                .UserTeams
                .Any(ut => ut.User.Username == username);
        }

        public bool IsTeamExisting(string teamName)
        {
            return context.Teams.Any(x => x.Name == teamName);
        }

        public bool IsUserCreatorOfEvent(string eventName, User user)
        {
            return context.Events
                .OrderByDescending(x => x.StartDate)
                .Where(x => x.Name == eventName)
                .Any(x=>x.CreatorId == user.Id);
        }

        public bool IsUserCreatorOfTeam(string teamName, User user)
        {
            return context.Teams
                .Any(x => x.Name == teamName && x.CreatorId == user.Id);
        }

        public bool IsUserExisting(string username)
        {
            return context.Users.Any(x => x.Username == username);
        }
    }
}
