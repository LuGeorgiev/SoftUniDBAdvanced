using System;
using System.Collections.Generic;
using System.Text;

namespace Stations.DataProcessor.Dto
{
    public class DelayedTrainsDto
    {
        public string TrainNumber { get; set; }
        public int DelayedTimes { get; set; } = 0;
        public TimeSpan MaxDelayedTime { get; set; }

    }
}
