using FindWayTesst.Solve;
using System;
using System.Text;

namespace FindWayTesst
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            FindWay findWay = new FindWay();
            findWay.ImportData();
            findWay.DFS();
            findWay.BFS();
            findWay.BestFirstSearch();
            findWay.LeoDoi();
            findWay.ASao();
            findWay.NhanhCan();


            CutAlpha alpha = new CutAlpha();
        }
    }
}
