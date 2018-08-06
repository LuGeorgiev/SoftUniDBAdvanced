using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeamBuilder.Models.Enumarations;

namespace TeamBuilder.Models
{
    public class User
    {
        public User()
        {
            this.CreatedEvents = new List<Event>();
            this.UserTeams = new List<UserTeam>();
            this.CreatedTeams = new List<Team>();
            this.ReceivedInvitations = new List<Invitation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25,MinimumLength =3)] 
        public string   Username { get; set; }

        [StringLength(25)]
        public string FirstName { get; set; }

        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, MinimumLength =6)] // TODO one digit and one Uppercase
        public string Password { get; set; }


        public Gender Gender { get; set; } 

        [Range(0, 2147483647)]
        public string Age { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Event> CreatedEvents { get; set; }
        public virtual ICollection<UserTeam> UserTeams { get; set; }
        public virtual ICollection<Team> CreatedTeams { get; set; }
        public virtual ICollection<Invitation> ReceivedInvitations { get; set; }
    }
}
