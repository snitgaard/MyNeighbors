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
        public double Rating { get; set; }
        public DateTime Date { get; set; }
        public double Noise_Rating { get; set; }
        public double Shopping_Rating { get; set; }
        public double Schools_Rating { get; set; }
        public int UserId { get; set; }


    }
}
