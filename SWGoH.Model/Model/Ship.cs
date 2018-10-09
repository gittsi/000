using System;
using System.Collections.Generic;
using System.Text;

namespace SWGoH.Model
{
    public class Ship : Unit
    {
        public List<Crew> Crew { get; set; }
    }

    public class Crew
    {
        public string Id { get; set; }
    }
}
