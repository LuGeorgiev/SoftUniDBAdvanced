

namespace Demo.Data.Models
{
    public  class PartNeeded
    {
        public int JobId { get; set; }
        public int PartId { get; set; }
        public int? Quantity { get; set; }

        public Job Job { get; set; }
        public Part Part { get; set; }
    }
}
