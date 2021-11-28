using FindWayTesst.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindWayTesst.Solve
{
    public class FindWay
    {
        public string Start { get; set; }
        public string End { get; set; }
        List<NodeLink> NodeLinks { get; set; }
        List<Node> Nodes { get; set; }

        public FindWay()
        {
            NodeLinks = new List<NodeLink>();
            Nodes = new List<Node>();
        }

        public void ImportData()
        {
            var text = System.IO.File.ReadAllLines(@"../../../File/Input.txt");
            var isFirstLine = true;
            foreach (var line in text)
            {
                var arr = line.Split(" ");
                if (isFirstLine)
                {
                    Start = arr[0];
                    End = arr[1];
                    isFirstLine = false;
                }
                else
                {
                    if (arr.Length == 2)
                    {
                        Nodes.Add(new Node()
                        {
                            Name = arr[0],
                            Weight = int.Parse(arr[1])
                        });
                    }
                    else
                    {
                        NodeLinks.Add(new NodeLink()
                        {
                            Start = Nodes.Where(x => x.Name == arr[0]).FirstOrDefault(),
                            End = Nodes.Where(x => x.Name == arr[1]).FirstOrDefault(),
                            Length = int.Parse(arr[2])
                        });
                    }
                }

            }

        }

        public void DFS()
        {
            PrintTable printTable = new PrintTable(@"../../../File/DFS.txt");
            printTable.PrintRow(new string[] { "Đỉnh", "Duyệt", "Dsach duyệt" });
            printTable.PrintLine();
            var path = new List<NodeLink>();
            var stack = new Stack<Node>();
            stack.Push(Nodes.FirstOrDefault(x => x.Name == Start));
            while (stack.Count() > 0)
            {
                var currentNode = stack.Pop();
                // đúng điểm cần tìm
                if (currentNode.Name == End)
                {
                    // in ra đường đi tại đây
                    var finded = End;
                    string pathRes = "";
                    while (true)
                    {
                        if (finded == Start)
                        {
                            pathRes += Start;
                            break;
                        }
                        pathRes += finded+"<--";
                        finded = path.FirstOrDefault(x => x.End.Name == finded).Start.Name;
                    }
                    printTable.PrintRow(new string[] { End, "TTKT", pathRes });
                    printTable.PrintLine();
                    break;
                }

                var nodeNext = NodeLinks.Where(x => x.Start.Name == currentNode.Name).OrderBy(x=>x.End.Name);
                foreach (var node in nodeNext)
                {
                    path.Add(new NodeLink()
                    {
                        Start = currentNode,
                        End = Nodes.FirstOrDefault(x => x.Name == node.End.Name),
                        Length = 0
                    });
                    stack.Push(Nodes.FirstOrDefault(x => x.Name == node.End.Name));
                }
                
                // cập nhật list L
                string listL = "";
                stack.ToList().ForEach(x => listL += x.Name + ",");

                // trạng thái kề

                string ttk = "";
                nodeNext.ToList().OrderBy(x=>x.End.Name).ToList().ForEach(x => ttk += x.End.Name + ",");

                printTable.PrintRow(new string[] { currentNode.Name,ttk , listL });
                printTable.PrintLine();
            }
            printTable.Close();
        }

        public void BFS()
        {
            PrintTable printTable = new PrintTable(@"../../../File/BFS.txt");
            printTable.PrintRow(new string[] { "Đỉnh", "Duyệt", "Dsach duyệt" });
            printTable.PrintLine();
            var path = new List<NodeLink>();
            var queue = new Queue<Node>();
            queue.Enqueue(Nodes.FirstOrDefault(x => x.Name == Start));
            while (queue.Count() > 0)
            {
                var currentNode = queue.Dequeue();
                // đúng điểm cần tìm
                if (currentNode.Name == End)
                {
                    // in ra đường đi tại đây
                    var finded = End;
                    string pathRes = "";
                    while (true)
                    {
                        if (finded == Start)
                        {
                            pathRes += Start;
                            break;
                        }
                        pathRes += finded + "<--";
                        finded = path.FirstOrDefault(x => x.End.Name == finded).Start.Name;
                    }
                    printTable.PrintRow(new string[] { End, "TTKT", pathRes });
                    printTable.PrintLine();
                    break;
                }

                var nodeNext = NodeLinks.Where(x => x.Start.Name == currentNode.Name).OrderBy(x => x.End.Name);
                foreach (var node in nodeNext)
                {
                    path.Add(new NodeLink()
                    {
                        Start = currentNode,
                        End = Nodes.FirstOrDefault(x => x.Name == node.End.Name),
                        Length = 0
                    });
                    queue.Enqueue(Nodes.FirstOrDefault(x => x.Name == node.End.Name));
                }
                string listL = "";
                queue.ToList().ForEach(x => listL += x.Name + ",");

                string ttk = "";
                nodeNext.ToList().OrderBy(x => x.End.Name).ToList().ForEach(x => ttk += x.End.Name + ",");

                printTable.PrintRow(new string[] { currentNode.Name, ttk, listL });
                printTable.PrintLine();
            }
            printTable.Close();
        }


        public void BestFirstSearch()
        {
            PrintTable printTable = new PrintTable(@"../../../File/BestFirstSearch.txt");
            printTable.PrintRow(new string[] { "Đỉnh", "Duyệt", "Dsach duyệt" });
            printTable.PrintLine();
            var path = new List<NodeLink>();
            var list = new List<Node>();
            list.Add(Nodes.FirstOrDefault(x => x.Name == Start));
            while (list.Count() > 0)
            {
                var currentNode = list.FirstOrDefault();
                list.Remove(currentNode);
                // đúng điểm cần tìm
                if (currentNode.Name == End)
                {
                    // in ra đường đi tại đây
                    var finded = End;
                    string pathRes = "";
                    while (true)
                    {
                        if (finded == Start)
                        {
                            pathRes += Start;
                            break;
                        }
                        pathRes += finded + "<--";
                        finded = path.FirstOrDefault(x => x.End.Name == finded).Start.Name;
                    }
                    printTable.PrintRow(new string[] { End, "TTKT", pathRes });
                    printTable.PrintLine();
                    break;
                }

                var nodeNext = NodeLinks.Where(x => x.Start.Name == currentNode.Name).OrderBy(x => x.End.Name);
                foreach (var node in nodeNext)
                {
                    path.Add(new NodeLink()
                    {
                        Start = currentNode,
                        End = Nodes.FirstOrDefault(x => x.Name == node.End.Name),
                        Length = 0
                    });
                    list.Add(Nodes.FirstOrDefault(x => x.Name == node.End.Name));
                }
                // sắp xếp lại danh sách L
                list = list.OrderBy(x => x.Weight).ToList();
                string listL = "";
                list.ForEach(x => listL += x.Name+x.Weight + ",");

                string ttk = "";
                nodeNext.ToList().ForEach(x => ttk += x.End.Name+x.End.Weight + ",");

                printTable.PrintRow(new string[] { currentNode.Name+currentNode.Weight, ttk, listL });
                printTable.PrintLine();
            }
            printTable.Close();
        }


        public void LeoDoi()
        {
            PrintTable printTable = new PrintTable(@"../../../File/LeoDoi.txt");
            printTable.PrintRow(new string[] { "Đỉnh", "TTK","L1", "L" });
            printTable.PrintLine();
            var path = new List<NodeLink>();
            var list = new List<Node>();
            list.Add(Nodes.FirstOrDefault(x => x.Name == Start));
            while (list.Count() > 0)
            {
                var currentNode = list.FirstOrDefault();
                list.Remove(currentNode);
                // đúng điểm cần tìm
                if (currentNode.Name == End)
                {
                    // in ra đường đi tại đây
                    var finded = End;
                    string pathRes = "";
                    while (true)
                    {
                        if (finded == Start)
                        {
                            pathRes += Start;
                            break;
                        }
                        pathRes += finded + "<--";
                        finded = path.FirstOrDefault(x => x.End.Name == finded).Start.Name;
                    }
                    printTable.PrintRow(new string[] { End, "TTKT","", pathRes });
                    printTable.PrintLine();
                    break;
                }

                var nodeNext = NodeLinks.Where(x => x.Start.Name == currentNode.Name).OrderBy(x => x.End.Name);
                var subList = new List<Node>();
                foreach (var node in nodeNext)
                {
                    path.Add(new NodeLink()
                    {
                        Start = currentNode,
                        End = Nodes.FirstOrDefault(x => x.Name == node.End.Name),
                        Length = 0
                    });
                    subList.Add(Nodes.FirstOrDefault(x => x.Name == node.End.Name));
                }

                subList = subList.OrderBy(x => x.Weight).ToList();


                // sắp xếp lại danh sách L
                list.InsertRange(0,subList);
                
                string listL = "";
                list.ForEach(x => listL += x.Name + x.Weight + ",");

                string ttk = "";
                nodeNext.ToList().ForEach(x => ttk += x.End.Name + x.End.Weight + ",");

                string L1 = "";
                subList.ToList().ForEach(x => L1 += x.Name + x.Weight + ",");

                printTable.PrintRow(new string[] { currentNode.Name + currentNode.Weight, ttk,L1, listL });
                printTable.PrintLine();
            }
            printTable.Close();
        }
    }
}
