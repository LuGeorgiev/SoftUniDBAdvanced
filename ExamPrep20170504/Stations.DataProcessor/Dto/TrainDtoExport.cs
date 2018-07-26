using System;
using System.Collections.Generic;
using System.Text;

namespace Stations.DataProcessor.Dto
{
    public class TrainDtoExport
    {
        public string TrainNumber { get; set; }
        public int DelayedTimes { get; set; }
        public string MaxDelayTime { get; set; }
    }
}
