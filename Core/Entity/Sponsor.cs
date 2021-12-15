using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeighbors.Core.Entity
{
    public class Sponsor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double X_coordinate { get; set; }
        public double Y_coordinate { get; set; }
        public string Type { get; set; }
    }
}
