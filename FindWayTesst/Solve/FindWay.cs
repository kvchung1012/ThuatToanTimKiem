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
                    stack.Push(Nodes.FirstOrDefault(x => x.Name == node.End.Name));
                }

                // cập nhật list L
                string listL = "";
                stack.ToList().ForEach(x => listL += x.Name + ",");

                // trạng thái kề

                string ttk = "";
                nodeNext.ToList().OrderBy(x => x.End.Name).ToList().ForEach(x => ttk += x.End.Name + ",");

                printTable.PrintRow(new string[] { currentNode.Name, ttk, listL });
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
                list.ForEach(x => listL += x.Name + x.Weight + ",");

                string ttk = "";
                nodeNext.ToList().ForEach(x => ttk += x.End.Name + x.End.Weight + ",");

                printTable.PrintRow(new string[] { currentNode.Name + currentNode.Weight, ttk, listL });
                printTable.PrintLine();
            }
            printTable.Close();
        }


        public void LeoDoi()
        {
            PrintTable printTable = new PrintTable(@"../../../File/LeoDoi.txt");
            printTable.PrintRow(new string[] { "Đỉnh", "TTK", "L1", "L" });
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
                    printTable.PrintRow(new string[] { End, "TTKT", "", pathRes });
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
                list.InsertRange(0, subList);

                string listL = "";
                list.ForEach(x => listL += x.Name + x.Weight + ",");

                string ttk = "";
                nodeNext.ToList().ForEach(x => ttk += x.End.Name + x.End.Weight + ",");

                string L1 = "";
                subList.ToList().ForEach(x => L1 += x.Name + x.Weight + ",");

                printTable.PrintRow(new string[] { currentNode.Name + currentNode.Weight, ttk, L1, listL });
                printTable.PrintLine();
            }
            printTable.Close();
        }


        public void ASao()
        {
            PrintTable printTable = new PrintTable(@"../../../File/ASao.txt");
            printTable.PrintRow(new string[] { "TT", "TTK", "k(u,v)", "h(v)", "g(v)", "f(v)", "Dsach L" });
            printTable.PrintLine();

            var list = new List<NodeAStar>(); // list duyệt đỉnh
            list.Add(new NodeAStar()
            {
                Name = Nodes.FirstOrDefault(x => x.Name == Start).Name,
                Depth = 0,
                Weight = Nodes.FirstOrDefault(x => x.Name == Start).Weight,
                NewWeight = 0
            }); // đỉnh đầu tiên

            var listStatus = new List<NodeLinkAStar>();

            while (list.Count() > 0)
            {
                var currentNode = list.FirstOrDefault();
                list.Remove(currentNode);
                // đúng điểm cần tìm
                var checkNode = currentNode;
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
                        finded = listStatus.Last(x => x.End.Name == finded).Start.Name;
                    }
                    printTable.PrintRow(new string[] { End, pathRes });
                    printTable.PrintLine();
                    break;
                }

                var nodeNext = NodeLinks.Where(x => x.Start.Name == currentNode.Name).OrderBy(x => x.End.Name);

                foreach (var node in nodeNext)
                {
                    listStatus.Add(new NodeLinkAStar()
                    {
                        Start = currentNode,
                        End = node.End,
                        Length = node.Length,
                        g = node.Length + currentNode.Depth,
                        f = node.Length + currentNode.Depth + node.End.Weight
                    });
                }

                // danh sách vừa thêm
                foreach (var item in listStatus.GetRange(listStatus.Count() - nodeNext.Count(), nodeNext.Count()))
                {
                    list.Add(new NodeAStar()
                    {
                        Name = item.End.Name,
                        Depth = item.g,
                        Weight = item.End.Weight,
                        NewWeight = item.f
                    });
                }
                list = list.OrderBy(x => x.NewWeight).ToList();


                // thêm vòng for in cho chất lượng
                var firstLine = true;
                foreach (var item in listStatus.GetRange(listStatus.Count() - nodeNext.Count(), nodeNext.Count()))
                {
                    if (firstLine)
                    {
                        string listL = "";
                        list.ForEach(x => listL += x.Name + x.NewWeight + ",");
                        printTable.PrintRow(new string[] { item.Start.Name, item.End.Name, item.Length.ToString(), item.End.Weight.ToString(), item.g.ToString(), item.f.ToString(), listL });
                        firstLine = false;
                    }
                    else
                    {
                        printTable.PrintRow(new string[] { "", item.End.Name, item.Length.ToString(), item.End.Weight.ToString(), item.g.ToString(), item.f.ToString(), "" });
                        firstLine = false;
                    }
                }
                printTable.PrintLine();
            }
            printTable.Close();
        }


        public void NhanhCan()
        {
            int cost = int.MaxValue;
            PrintTable printTable = new PrintTable(@"../../../File/NhanhCan.txt");
            printTable.PrintRow(new string[] { "TT", "TTK", "k(u,v)", "h(v)", "g(v)", "f(v)", "Dsach L1", "Dsach L" });
            printTable.PrintLine();

            var list = new List<NodeAStar>(); // list duyệt đỉnh
            list.Add(new NodeAStar()
            {
                Name = Nodes.FirstOrDefault(x => x.Name == Start).Name,
                Depth = 0,
                Weight = Nodes.FirstOrDefault(x => x.Name == Start).Weight,
                NewWeight = 0
            }); // đỉnh đầu tiên

            var listStatus = new List<NodeLinkAStar>();

            while (list.Count() > 0)
            {
                var currentNode = list.FirstOrDefault();
                list.Remove(currentNode);
                // đúng điểm cần tìm
                var checkNode = currentNode;
                if (currentNode.Name == End)
                {
                    if(currentNode.NewWeight <= cost)
                    {
                        cost = currentNode.NewWeight;
                        string listL = "";
                        list.ForEach(x => listL += x.Name + x.NewWeight + ",");
                        printTable.PrintRow(new string[] { End, "Tạm, độ dài " + currentNode.NewWeight, listL });
                    }
                    else
                    {
                        break;
                    }
                }

                var nodeNext = NodeLinks.Where(x => x.Start.Name == currentNode.Name).OrderBy(x => x.End.Name);

                foreach (var node in nodeNext)
                {
                    listStatus.Add(new NodeLinkAStar()
                    {
                        Start = currentNode,
                        End = node.End,
                        Length = node.Length,
                        g = node.Length + currentNode.Depth,
                        f = node.Length + currentNode.Depth + node.End.Weight
                    });
                }

                // danh sách vừa thêm
                var currentList = new List<NodeAStar>();
                foreach (var item in listStatus.GetRange(listStatus.Count() - nodeNext.Count(), nodeNext.Count()))
                {
                    currentList.Add(new NodeAStar()
                    {
                        Name = item.End.Name,
                        Depth = item.g,
                        Weight = item.End.Weight,
                        NewWeight = item.f
                    });
                }

                list.InsertRange(0, currentList.OrderBy(x => x.NewWeight).ToList());


                // thêm vòng for in cho chất lượng
                var firstLine = true;
                foreach (var item in listStatus.GetRange(listStatus.Count() - nodeNext.Count(), nodeNext.Count()))
                {
                    if (firstLine)
                    {
                        string listL = "";
                        list.ForEach(x => listL += x.Name + x.NewWeight + ",");

                        string list1 = "";
                        currentList.OrderBy(x => x.NewWeight).ToList().ForEach(x => list1 += x.Name + x.NewWeight + ",");

                        printTable.PrintRow(new string[] { item.Start.Name, item.End.Name, item.Length.ToString(), item.End.Weight.ToString(), item.g.ToString(), item.f.ToString(), list1, listL });
                        firstLine = false;
                    }
                    else
                    {
                        printTable.PrintRow(new string[] { "", item.End.Name, item.Length.ToString(), item.End.Weight.ToString(), item.f.ToString(), item.f.ToString(), "", "" });
                        firstLine = false;
                    }
                }
                printTable.PrintLine();
            }
            printTable.Close();
        }
    }
}
