using CutAlpha.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CutAlpha
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonString = File.ReadAllText(@"C:\Users\Delll\source\repos\FindWayTesst\CutAlpha\File\Input.json");
            var data = JsonConvert.DeserializeObject<List<List<int>>>(jsonString);
            Console.WriteLine("Hello World!");
        }
    }
}
