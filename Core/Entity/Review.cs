using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeighbors.Core.Entity
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; } 
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public int Noise_Rating { get; set; }
        public int Shopping_Rating { get; set; }
        public int Schools_Rating { get; set; }
        public int UserId { get; set; }
        public string AddressId { get; set; }

    }
}
