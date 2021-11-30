using System;
using System.Collections.Generic;
using System.Text;

namespace FindWayTesst.Model
{
    public class NodeAStar : Node
    {
        public int Depth { get; set; }
        public int NewWeight { get; set; }
    }
}
