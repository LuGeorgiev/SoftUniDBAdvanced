using Instagraph.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Instagraph.DataProcessor.ModelsDto
{
    public class UserFollowerDto
    {
        public string User { get; set; }
        
        public string Follower { get; set; }
    }
}
