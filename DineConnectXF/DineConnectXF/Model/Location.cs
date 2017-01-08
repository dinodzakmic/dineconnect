using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineConnectXF.Model
{
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public string ValidationErrors { get; set; }
    }

    public class Location
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class LocationResult
    {
        public List<Location> Items { get; set; }
    }

    public class Locations
    {
        public bool Success { get; set; }
        public LocationResult Result { get; set; }
        public Error Error { get; set; }
        public bool UnAuthorizedRequest { get; set; }
    }
}
