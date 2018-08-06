using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeamBuilder.Models
{
    public class Team
    {
        public Team()
        {
            this.UserTeams = new List<UserTeam>();
            this.Events = new List<EventTeam>();
            this.Invittaions = new List<Invitation>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; } 

        [MaxLength(32)]
        public string Description { get; set; }

        [Required]
        [StringLength(3,MinimumLength =3)]
        public string Acronym { get; set; }

        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<UserTeam> UserTeams { get; set; }
        public virtual ICollection<EventTeam> Events { get; set; }
        public virtual ICollection<Invitation> Invittaions { get; set; }

    }
}
