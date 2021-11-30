using System;
using System.Collections.Generic;
using System.Text;

namespace FindWayTesst.Model
{
    class NodeLinkAStar : NodeLink
    {
        public int g { get; set; }
        public int f { get; set; }
    }
}
