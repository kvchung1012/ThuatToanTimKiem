using System;
using System.Collections.Generic;
using System.Text;

namespace FindWayTesst.Model
{
    public class CutAlphaModel
    {
        public string Name { get; set; }
        public int Val { get; set; }
        public ChangeNode changeNodes { get; set; } = new ChangeNode();
        public List<CutAlphaModel> childs { get; set; } = new List<CutAlphaModel>();
    }

    public class ChangeNode
    {
        public List<int> Val { get; set; } = new List<int>();
        public List<int> Alpha { get; set; } = new List<int>();
        public List<int> Beta { get; set; } = new List<int>();
    }
}
