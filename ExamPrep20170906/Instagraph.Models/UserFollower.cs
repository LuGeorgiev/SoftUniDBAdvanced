

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagraph.Models
{
    public class UserFollower
    {
        [Key]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Key]
        public int FollowerId  { get; set; }
        public virtual User Follower  { get; set; }
    }
}
