using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeamBuilder.Models
{
    public class Event
    {
        public Event()
        {
            this.ParticipatingEventTeams = new List<EventTeam>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; } //TODO to be after startDate

        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<EventTeam> ParticipatingEventTeams { get; set; }

    }
}
