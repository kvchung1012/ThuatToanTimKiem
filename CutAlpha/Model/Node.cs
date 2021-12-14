using System;
using System.Collections.Generic;
using System.Text;

namespace CutAlpha.Model
{
    public class Node
    {
        public int Number { get; set; }
        public List<Node> Childs { get; set; }
    }
}
