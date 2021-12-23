using FindWayTesst.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindWayTesst.Solve
{
    public class CutAlpha
    {
        public bool MAX = false;
        public List<CutAlphaModel> nodes { get; set; } = new List<CutAlphaModel>();

        Dictionary<string, List<int>> dics = new Dictionary<string, List<int>>();


        public CutAlpha()
        {
            dics.Add("e", new List<int>() { 2, 6 });
            dics.Add("f", new List<int>() { 5, 6,3 });
            dics.Add("c", new List<int>() { 7, 8 });
            dics.Add("g", new List<int>() { 9, 1, 3 });
            dics.Add("h", new List<int>() { 5,2, 4 });
            dics.Add("i", new List<int>() { 7 });
            dics.Add("j", new List<int>() { 9, 4 });


            nodes.Add(new CutAlphaModel()
            {
                Name = "ROOT"
            });
            // import node
            foreach (var item in System.IO.File.ReadAllLines(@"../../../File/tree_view.txt"))
            {
                if (item.Equals("")) break;
                var list = item.Split(' ');
                // thêm node cha
                AddToTree(list[0], list.Where(x => x != list[0]).ToList(), nodes);
            }

            int alpha = -1000;
            int beta = 1000;
            Console.WriteLine("Kết quả: " + MiniMax(nodes.First(),  alpha,  beta, MAX));

            PrintStep(nodes.FirstOrDefault(), "");
        }

        public void AddToTree(string name, List<string> childs, List<CutAlphaModel> listData)
        {
            // kiểm tra list đã tồn tại node chưa
            // chưa tồn tại node
            if (listData.FirstOrDefault(x => x.Name == name) == null)
            {
                foreach (var item in listData)
                {
                    AddToTree(name, childs, item.childs);
                }
            }

            // đã tồn tại node
            // add list childs
            else
            {
                foreach (var item in childs)
                {

                    // tồn tại node con

                    var node = new CutAlphaModel()
                    {
                        Name = item
                    };
                    if (dics.ContainsKey(item))
                    {
                        foreach (var num in dics.FirstOrDefault(x => x.Key == item).Value)
                        {
                            node.childs.Add(new CutAlphaModel()
                            {
                                Name = num.ToString(),
                                Val = num
                            });
                        }
                    }

                    listData.FirstOrDefault(x => x.Name == name).childs.Add(node);
                }
                return;
            }
        }

        public int MiniMax(CutAlphaModel cutAlpha, int alpha, int beta, bool isMax)
        {
            // đây là đỉnh max
            if (isMax)
            {
                // điểm kết thúc
                if (cutAlpha.childs.Count() == 0) return cutAlpha.Val;

                cutAlpha.Val = -1000;

                cutAlpha.changeNodes.Val.Add(cutAlpha.Val);
                cutAlpha.changeNodes.Alpha.Add(alpha);
                cutAlpha.changeNodes.Beta.Add(beta);

                foreach (var item in cutAlpha.childs)
                {
                    cutAlpha.Val = Math.Max(cutAlpha.Val, MiniMax(item, alpha,beta, false));

                    // val mới lớn hơn value cũ
                    if (cutAlpha.Val > cutAlpha.changeNodes.Val.Last())
                    {
                        cutAlpha.changeNodes.Val.Add(cutAlpha.Val);
                    }

                    // cắt tỉa
                    if (cutAlpha.Val >= beta)
                    {
                        Console.WriteLine($"Tỉa các đoạn từ {cutAlpha.Name} -> {item.Name}");
                        return cutAlpha.Val;
                    }


                    alpha = Math.Max(alpha, cutAlpha.Val);
                    // nếu alpha 
                    if (alpha > cutAlpha.changeNodes.Alpha.Last())
                    {
                        cutAlpha.changeNodes.Alpha.Add(alpha);
                    }
                }
                return cutAlpha.Val;
            }
            else
            {
                if (cutAlpha.childs.Count() == 0) return cutAlpha.Val;
                cutAlpha.Val = 1000;

                cutAlpha.changeNodes.Val.Add(cutAlpha.Val);
                cutAlpha.changeNodes.Alpha.Add(alpha);
                cutAlpha.changeNodes.Beta.Add(beta);

                foreach (var item in cutAlpha.childs)
                {
                    cutAlpha.Val = Math.Min(cutAlpha.Val, MiniMax(item,  alpha, beta, true));

                    // thay đổi val
                    if (cutAlpha.Val < cutAlpha.changeNodes.Val.Last())
                    {
                        cutAlpha.changeNodes.Val.Add(cutAlpha.Val);
                    }


                    if (cutAlpha.Val <= alpha)
                    {
                        Console.WriteLine($"Tỉa các đoạn từ {cutAlpha.Name} -> {item.Name}");
                        return cutAlpha.Val;
                    }

                    beta = Math.Min(beta, cutAlpha.Val);

                    // thay dổi beta
                    if (beta < cutAlpha.changeNodes.Beta.Last())
                    {
                        cutAlpha.changeNodes.Beta.Add(beta);
                    }

                }
                return cutAlpha.Val;
            }
        }


        public void PrintStep(CutAlphaModel cutAlpha, string tab)
        {
            if (!int.TryParse(cutAlpha.Name, out int n))
            {
                Console.WriteLine("\n" + tab + "-----------");
                Console.WriteLine(tab + "Node: " + cutAlpha.Name);
                Console.WriteLine(tab + "Val: " + String.Join(",", cutAlpha.changeNodes.Val.ToArray()));
                Console.WriteLine(tab + "Alpha: " + String.Join(",", cutAlpha.changeNodes.Alpha.ToArray()));
                Console.WriteLine(tab + "Beta: " + String.Join(",", cutAlpha.changeNodes.Beta.ToArray()));

                if (cutAlpha.childs.Count() > 0)
                {
                    foreach (var item in cutAlpha.childs)
                    {
                        PrintStep(item, (tab + "\t"));
                    }
                }
            }

        }
    }
}
