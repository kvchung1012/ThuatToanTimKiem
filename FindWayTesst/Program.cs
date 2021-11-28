using FindWayTesst.Solve;
using System;

namespace FindWayTesst
{
    class Program
    {
        static void Main(string[] args)
        {
            FindWay findWay = new FindWay();
            findWay.ImportData();
            findWay.DFS();
            findWay.BFS();
            findWay.BestFirstSearch();
            findWay.LeoDoi();
        }
    }
}
